<script lang="ts">
	import * as Card from '$lib/components/ui/card';
	import { Button } from '$lib/components/ui/button';
	import * as Dialog from '$lib/components/ui/dialog';
	import { Separator } from '$lib/components/ui/separator';
	import { browserClient, getErrorMessage } from '$lib/api';
	import { toast } from '$lib/components/ui/sonner';
	import { goto, invalidateAll } from '$app/navigation';
	import { resolve } from '$app/paths';
	import { Lock, Unlock, Trash2, Loader2 } from '@lucide/svelte';
	import type { AdminUser, User } from '$lib/types';
	import { canManageUser } from '$lib/utils';
	import * as m from '$lib/paraglide/messages';

	interface Props {
		user: AdminUser;
		currentUser: User;
	}

	let { user, currentUser }: Props = $props();

	let deleteDialogOpen = $state(false);
	let isLocking = $state(false);
	let isUnlocking = $state(false);
	let isDeleting = $state(false);

	let callerRoles = $derived(currentUser.roles ?? []);
	let targetRoles = $derived(user.roles ?? []);
	let canManage = $derived(canManageUser(callerRoles, targetRoles));

	async function lockUser() {
		isLocking = true;
		const { response, error } = await browserClient.POST('/api/v1/admin/users/{id}/lock', {
			params: { path: { id: user.id ?? '' } }
		});
		isLocking = false;

		if (response.ok) {
			toast.success(m.admin_userDetail_lockSuccess());
			await invalidateAll();
		} else {
			toast.error(getErrorMessage(error, m.admin_userDetail_lockError()));
		}
	}

	async function unlockUser() {
		isUnlocking = true;
		const { response, error } = await browserClient.POST('/api/v1/admin/users/{id}/unlock', {
			params: { path: { id: user.id ?? '' } }
		});
		isUnlocking = false;

		if (response.ok) {
			toast.success(m.admin_userDetail_unlockSuccess());
			await invalidateAll();
		} else {
			toast.error(getErrorMessage(error, m.admin_userDetail_unlockError()));
		}
	}

	async function deleteUser() {
		isDeleting = true;
		const { response, error } = await browserClient.DELETE('/api/v1/admin/users/{id}', {
			params: { path: { id: user.id ?? '' } }
		});
		isDeleting = false;
		deleteDialogOpen = false;

		if (response.ok) {
			toast.success(m.admin_userDetail_deleteSuccess());
			await goto(resolve('/admin/users'));
		} else {
			toast.error(getErrorMessage(error, m.admin_userDetail_deleteError()));
		}
	}
</script>

<Card.Root>
	<Card.Header>
		<Card.Title>{m.admin_userDetail_accountActions()}</Card.Title>
		<Card.Description>{m.admin_userDetail_accountActionsDescription()}</Card.Description>
	</Card.Header>
	<Card.Content>
		{#if canManage}
			<div class="space-y-4">
				<!-- Lock/Unlock -->
				<div>
					{#if user.isLockedOut}
						<Button variant="outline" disabled={isUnlocking} onclick={unlockUser}>
							{#if isUnlocking}
								<Loader2 class="me-2 h-4 w-4 animate-spin" />
							{:else}
								<Unlock class="me-2 h-4 w-4" />
							{/if}
							{m.admin_userDetail_unlockAccount()}
						</Button>
					{:else}
						<Button variant="outline" disabled={isLocking} onclick={lockUser}>
							{#if isLocking}
								<Loader2 class="me-2 h-4 w-4 animate-spin" />
							{:else}
								<Lock class="me-2 h-4 w-4" />
							{/if}
							{m.admin_userDetail_lockAccount()}
						</Button>
					{/if}
				</div>

				<Separator />

				<!-- Danger Zone -->
				<div class="space-y-3">
					<p class="text-sm font-medium text-destructive">{m.admin_userDetail_dangerZone()}</p>
					<Dialog.Root bind:open={deleteDialogOpen}>
						<Dialog.Trigger>
							{#snippet child({ props })}
								<Button variant="destructive" {...props}>
									<Trash2 class="me-2 h-4 w-4" />
									{m.admin_userDetail_deleteAccount()}
								</Button>
							{/snippet}
						</Dialog.Trigger>
						<Dialog.Content>
							<Dialog.Header>
								<Dialog.Title>{m.admin_userDetail_deleteConfirmTitle()}</Dialog.Title>
								<Dialog.Description>
									{m.admin_userDetail_deleteConfirmDescription()}
								</Dialog.Description>
							</Dialog.Header>
							<Dialog.Footer class="flex-col-reverse sm:flex-row">
								<Button variant="outline" onclick={() => (deleteDialogOpen = false)}>
									{m.admin_userDetail_deleteCancel()}
								</Button>
								<Button variant="destructive" disabled={isDeleting} onclick={deleteUser}>
									{#if isDeleting}
										<Loader2 class="me-2 h-4 w-4 animate-spin" />
									{/if}
									{m.admin_userDetail_deleteConfirm()}
								</Button>
							</Dialog.Footer>
						</Dialog.Content>
					</Dialog.Root>
				</div>
			</div>
		{:else}
			<p class="text-sm text-muted-foreground">
				{m.admin_userDetail_cannotManage()}
			</p>
		{/if}
	</Card.Content>
</Card.Root>
