export const Routes = {
    HOME: '/',
    SIGN_IN: '/auth/login',
    NOT_FOUND: '/not-found',
    FORBIDDEN: '/forbidden',
    DASHBOARD: '/dashboard',
    // PROFILE: (id: string) => `/profile/${id}`, // Dynamic route example
    // AUTH: { LOGIN: '/auth/login', }, // child level example
} as const;

// * Example
// Routes.PROFILE('123') Routes.HOME
// typeof route === 'function' ? route('123') : route