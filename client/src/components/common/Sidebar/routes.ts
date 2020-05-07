export const routes = [
    {
        path: '/table',
        name: 'Tables',
        icon: 'fas fa-table',
    },
    {
        path: '/form',
        name: 'Forms',
        icon: 'far fa-check-square',
    },
    {
        path: '/pages',
        name: 'Pages',
        icon: 'fas fa-copy',
        subList: [
            {
                path: '/login',
                name: 'Login'
            },
            {
                path: '/register',
                name: 'Register'
            },
            {
                path: '/forget-pass',
                name: 'Forgot Password'
            },
        ]
    },
]