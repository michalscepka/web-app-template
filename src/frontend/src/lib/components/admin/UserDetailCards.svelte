<script lang="ts">
	import { AccountInfoCard, RoleManagementCard, AccountActionsCard } from '$lib/components/admin';
	import type { AdminUser, AdminRole, User } from '$lib/types';

	interface Props {
		user: AdminUser;
		roles: AdminRole[];
		currentUser: User;
	}

	let { user, roles, currentUser }: Props = $props();
</script>

<div class="grid gap-6">
	<AccountInfoCard {user} />
	<RoleManagementCard {user} {roles} {currentUser} />
	<AccountActionsCard {user} {currentUser} />
</div>
