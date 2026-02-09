<script lang="ts">
	import { Badge } from '$lib/components/ui/badge';
	import type { AdminRole } from '$lib/types';
	import * as m from '$lib/paraglide/messages';

	interface Props {
		roles: AdminRole[];
	}

	let { roles }: Props = $props();
</script>

<div class="overflow-x-auto">
	<table class="w-full text-sm">
		<thead>
			<tr class="border-b text-start">
				<th class="px-3 py-3 text-start font-medium text-muted-foreground">
					{m.admin_roles_name()}
				</th>
				<th class="px-3 py-3 text-end font-medium text-muted-foreground">
					{m.admin_roles_userCount()}
				</th>
			</tr>
		</thead>
		<tbody>
			{#each roles as role (role.id)}
				<tr class="border-b transition-colors hover:bg-muted/50">
					<td class="px-3 py-3">
						<Badge variant="secondary">{role.name}</Badge>
					</td>
					<td class="px-3 py-3 text-end tabular-nums">{role.userCount ?? 0}</td>
				</tr>
			{:else}
				<tr>
					<td colspan="2" class="py-8 text-center text-muted-foreground">
						{m.admin_roles_noResults()}
					</td>
				</tr>
			{/each}
		</tbody>
	</table>
</div>
