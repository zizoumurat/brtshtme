/** @type {import('tailwindcss').Config} */
import PrimeUI from 'tailwindcss-primeui';

export default {
    darkMode: ['selector', '[class*="app-dark"]'],
    content: ['./src/**/*.{html,js,ts}', './public/**/*.json'],
    plugins: [PrimeUI],
    theme: {
        extend: {
            boxShadow: {
                v1: 'var(--shadow-v1)',
                v2: 'var(--shadow-v2)',
                'custom-shadow': 'var(--custom-shadow)'
            },
            opacity: {
                4: '0.04',
                6: '0.06',
                8: '0.08',
                12: '0.12',
                16: '0.16',
                32: '0.32',
                36: '0.36',
                48: '0.48',
                64: '0.64',
                72: '0.72'
            },
            animation: {
                'infinite-scroll': 'infinite-scroll 25s linear infinite',
                float: 'float 3.5s ease-in-out infinite',
                wiggle: 'wiggle 3.5s ease-in-out infinite'
            },
            keyframes: {
                'infinite-scroll': {
                    from: {transform: 'translateX(0)'},
                    to: {transform: 'translateX(-100%)'}
                },
                float: {
                    '0%, 100%': {transform: 'translateY(0)'},
                    '50%': {transform: 'translateY(-6px) scale(1.005)'}
                },
                wiggle: {
                    '0%, 30%, 100%': {
                        transform: 'rotate(0deg)'
                    },
                    '10%, 20%': {
                        transform: 'rotate(-1deg)'
                    },
                    '15%, 25%': {
                        transform: 'rotate(1deg)'
                    }
                }
            }
        },
        screens: {
            sm: '576px',
            md: '768px',
            lg: '992px',
            xl: '1200px',
            '2xl': '1920px'
        }
    }
};
