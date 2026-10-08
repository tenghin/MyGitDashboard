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

export async function saveScore(score) {

    const { data: userData, error: userError } =
        await supabase.auth.getUser();

    if (userError || !userData.user) {
        return;
    }

    const user = userData.user;


    // Get the player's existing high score.
    const { data: player, error: readError } =
        await supabase
            .from('tetris_players')
            .select('high_score')
            .eq('user_id', user.id)
            .single();

    if (readError) {
        console.error(
            'Failed to read score:',
            readError
        );

        return;
    }


    const oldHighScore =
        player.high_score ?? 0;

    const newHighScore =
        Math.max(oldHighScore, score);


    // Save current score + best score.
    const { error: updateError } =
        await supabase
            .from('tetris_players')
            .update({
                latest_score: score,
                high_score: newHighScore
            })
            .eq('user_id', user.id);


    if (updateError) {
        console.error(
            'Failed to save score:',
            updateError
        );
    }
}

export async function getLeaderboard() {

    const { data, error } =
        await supabase
            .from('tetris_players')
            .select(
                'github_username, avatar_url, high_score'
            )
            .order(
                'high_score',
                { ascending: false }
            )
            .limit(10);

    if (error) {
        console.error(
            'Failed to load leaderboard:',
            error
        );

        return [];
    }


    return data.map(player => ({
        username:
            player.github_username,

        avatarUrl:
            player.avatar_url ?? '',

        highScore:
            player.high_score ?? 0
    }));
}

export async function signOut() {

    await supabase.auth.signOut();
}