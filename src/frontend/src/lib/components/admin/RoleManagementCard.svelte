<script lang="ts">
	import * as Card from '$lib/components/ui/card';
	import { Badge } from '$lib/components/ui/badge';
	import { Button } from '$lib/components/ui/button';
	import * as Select from '$lib/components/ui/select';
	import { browserClient, getErrorMessage } from '$lib/api';
	import { toast } from '$lib/components/ui/sonner';
	import { invalidateAll } from '$app/navigation';
	import { Plus, X, Loader2 } from '@lucide/svelte';
	import type { AdminUser, AdminRole, User } from '$lib/types';
	import { canManageUser, getAssignableRoles, getRoleRank, getHighestRank } from '$lib/utils';
	import * as m from '$lib/paraglide/messages';

	interface Props {
		user: AdminUser;
		roles: AdminRole[];
		currentUser: User;
	}

	let { user, roles, currentUser }: Props = $props();

	let isAssigningRole = $state(false);
	let isRemovingRole = $state<string | null>(null);

	let callerRoles = $derived(currentUser.roles ?? []);
	let targetRoles = $derived(user.roles ?? []);
	let canManage = $derived(canManageUser(callerRoles, targetRoles));
	let callerRank = $derived(getHighestRank(callerRoles));

	let availableRoles = $derived(
		getAssignableRoles(
			callerRoles,
			(roles ?? []).map((r) => r.name ?? '')
		).filter((role) => !targetRoles.includes(role))
	);

	function canRemoveRole(role: string): boolean {
		return canManage && getRoleRank(role) < callerRank;
	}

	let selectedRole = $state('');

	async function assignRole() {
		if (!selectedRole) return;
		isAssigningRole = true;
		const { response, error } = await browserClient.POST('/api/v1/admin/users/{id}/roles', {
			params: { path: { id: user.id ?? '' } },
			body: { role: selectedRole }
		});
		isAssigningRole = false;

		if (response.ok) {
			toast.success(m.admin_userDetail_roleAssigned());
			selectedRole = '';
			await invalidateAll();
		} else {
			toast.error(getErrorMessage(error, m.admin_userDetail_roleAssignError()));
		}
	}

	async function removeRole(role: string) {
		isRemovingRole = role;
		const { response, error } = await browserClient.DELETE(
			'/api/v1/admin/users/{id}/roles/{role}',
			{
				params: { path: { id: user.id ?? '', role } }
			}
		);
		isRemovingRole = null;

		if (response.ok) {
			toast.success(m.admin_userDetail_roleRemoved());
			await invalidateAll();
		} else {
			toast.error(getErrorMessage(error, m.admin_userDetail_roleRemoveError()));
		}
	}
</script>

<Card.Root>
	<Card.Header>
		<Card.Title>{m.admin_userDetail_roleManagement()}</Card.Title>
		<Card.Description>{m.admin_userDetail_roleManagementDescription()}</Card.Description>
	</Card.Header>
	<Card.Content class="space-y-4">
		<div>
			<p class="mb-2 text-sm font-medium">{m.admin_userDetail_currentRoles()}</p>
			<div class="flex flex-wrap gap-2">
				{#each user.roles ?? [] as role (role)}
					<Badge variant="secondary" class="gap-1 py-1 text-sm">
						{role}
						{#if canRemoveRole(role)}
							<button
								class="ms-1 inline-flex h-5 w-5 items-center justify-center rounded-full transition-colors hover:bg-muted-foreground/20"
								aria-label="{m.admin_userDetail_removeRole()} {role}"
								disabled={isRemovingRole === role}
								onclick={() => removeRole(role)}
							>
								{#if isRemovingRole === role}
									<Loader2 class="h-3 w-3 animate-spin" />
								{:else}
									<X class="h-3 w-3" />
								{/if}
							</button>
						{/if}
					</Badge>
				{:else}
					<span class="text-sm text-muted-foreground">{m.admin_userDetail_noRoles()}</span>
				{/each}
			</div>
		</div>

		{#if canManage && availableRoles.length > 0}
			<div class="flex flex-col gap-2 sm:flex-row sm:items-end">
				<div class="flex-1">
					<label for="role-select" class="mb-1 block text-sm font-medium">
						{m.admin_userDetail_assignRole()}
					</label>
					<Select.Root type="single" bind:value={selectedRole}>
						<Select.Trigger id="role-select">
							{#if selectedRole}
								<span>{selectedRole}</span>
							{:else}
								<span class="text-muted-foreground">{m.admin_userDetail_selectRole()}</span>
							{/if}
						</Select.Trigger>
						<Select.Content>
							{#each availableRoles as role (role)}
								<Select.Item value={role}>{role}</Select.Item>
							{/each}
						</Select.Content>
					</Select.Root>
				</div>
				<Button
					size="sm"
					class="min-h-10 shrink-0"
					disabled={!selectedRole || isAssigningRole}
					onclick={assignRole}
				>
					{#if isAssigningRole}
						<Loader2 class="me-1 h-4 w-4 animate-spin" />
					{:else}
						<Plus class="me-1 h-4 w-4" />
					{/if}
					{m.admin_userDetail_assignRole()}
				</Button>
			</div>
		{/if}
	</Card.Content>
</Card.Root>
