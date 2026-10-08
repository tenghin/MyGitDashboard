import { createClient } from
    'https://cdn.jsdelivr.net/npm/@supabase/supabase-js/+esm';

const supabase = createClient(
    'https://jhbkxhvhybwchtgldmdc.supabase.co',
    'sb_publishable_x3DcSKT_bp7lECnuWg4P4Q_aQ_3FNLi'
);

export async function signInWithGitHub() {

    const redirectTo =
        new URL('tetris', document.baseURI).href;

    const { error } =
        await supabase.auth.signInWithOAuth({
            provider: 'github',
            options: {
                redirectTo: redirectTo
            }
        });

    if (error) {
        console.error(error);
    }
}


export async function getCurrentUser() {

    const { data, error } =
        await supabase.auth.getUser();

    if (error || !data.user) {
        return null;
    }

    const user = data.user;

    const username =
        user.user_metadata.user_name ??
        user.user_metadata.preferred_username ??
        'GitHub User';

    const avatarUrl =
        user.user_metadata.avatar_url ?? null;


    // Record/update this GitHub user in our table.
    const { error: playerError } =
        await supabase
            .from('tetris_players')
            .upsert(
                {
                    user_id: user.id,
                    github_username: username,
                    avatar_url: avatarUrl
                },
                {
                    onConflict: 'user_id'
                }
            );


    if (playerError) {
        console.error(
            'Failed to save player:',
            playerError
        );
    }


    return {
        id: user.id,
        username: username,
        avatarUrl: avatarUrl ?? ''
    };
}


export async function signOut() {

    await supabase.auth.signOut();
}