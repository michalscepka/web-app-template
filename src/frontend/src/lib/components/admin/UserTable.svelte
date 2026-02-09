<script lang="ts">
	import { resolve } from '$app/paths';
	import { Badge } from '$lib/components/ui/badge';
	import { buttonVariants } from '$lib/components/ui/button';
	import { Eye } from '@lucide/svelte';
	import type { AdminUser } from '$lib/types';
	import * as m from '$lib/paraglide/messages';
	import { cn } from '$lib/utils';

	interface Props {
		users: AdminUser[];
	}

	let { users }: Props = $props();
</script>

<!-- eslint-disable svelte/no-navigation-without-resolve -- hrefs are pre-resolved using resolve() -->
<div class="overflow-x-auto">
	<table class="w-full text-sm">
		<thead>
			<tr class="border-b text-start">
				<th class="px-3 py-3 text-start font-medium text-muted-foreground"
					>{m.admin_users_name()}</th
				>
				<th class="px-3 py-3 text-start font-medium text-muted-foreground"
					>{m.admin_users_email()}</th
				>
				<th class="hidden px-3 py-3 text-start font-medium text-muted-foreground sm:table-cell">
					{m.admin_users_roles()}
				</th>
				<th class="hidden px-3 py-3 text-start font-medium text-muted-foreground md:table-cell">
					{m.admin_users_status()}
				</th>
				<th class="px-3 py-3 text-end font-medium text-muted-foreground"
					>{m.admin_users_actions()}</th
				>
			</tr>
		</thead>
		<tbody>
			{#each users as user (user.id)}
				<tr class="border-b transition-colors hover:bg-muted/50">
					<td class="px-3 py-3 font-medium">
						{#if user.firstName || user.lastName}
							{[user.firstName, user.lastName].filter(Boolean).join(' ')}
						{:else}
							<span class="text-muted-foreground">{user.username}</span>
						{/if}
					</td>
					<td class="px-3 py-3 text-muted-foreground">
						<span class="truncate">{user.email}</span>
					</td>
					<td class="hidden px-3 py-3 sm:table-cell">
						<div class="flex flex-wrap gap-1">
							{#each user.roles ?? [] as role (role)}
								<Badge variant="secondary" class="text-xs">{role}</Badge>
							{:else}
								<span class="text-xs text-muted-foreground">&mdash;</span>
							{/each}
						</div>
					</td>
					<td class="hidden px-3 py-3 md:table-cell">
						{#if user.isLockedOut}
							<Badge variant="destructive" class="text-xs">{m.admin_users_locked()}</Badge>
						{:else}
							<Badge variant="outline" class="text-xs">{m.admin_users_active()}</Badge>
						{/if}
					</td>
					<td class="px-3 py-3 text-end">
						<a
							href={resolve(`/admin/users/${user.id}`)}
							class={cn(buttonVariants({ variant: 'ghost', size: 'icon' }), 'h-8 w-8')}
							aria-label={m.admin_users_viewDetails()}
						>
							<Eye class="h-4 w-4" />
						</a>
					</td>
				</tr>
			{:else}
				<tr>
					<td colspan="5" class="py-8 text-center text-muted-foreground">
						{m.admin_users_noResults()}
					</td>
				</tr>
			{/each}
		</tbody>
	</table>
</div>
