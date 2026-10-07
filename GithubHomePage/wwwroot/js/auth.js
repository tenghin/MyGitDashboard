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

    return {
        id: user.id,

        username:
            user.user_metadata.user_name ??
            user.user_metadata.preferred_username ??
            'GitHub User',

        avatarUrl:
            user.user_metadata.avatar_url ?? ''
    };
}


export async function signOut() {

    await supabase.auth.signOut();
}