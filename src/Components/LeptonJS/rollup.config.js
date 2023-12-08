import filesize from "rollup-plugin-filesize";
import terser from "@rollup/plugin-terser";
import typescript from "@rollup/plugin-typescript";
import dts from 'rollup-plugin-dts';

const plugins = [
    typescript({
        compilerOptions: {
            declaration: false,
            declarationDir: undefined,
        },
    }),
    filesize({
        showMinifiedSize: false,
        showBrotliSize: true,
    }),
];

export default [
    {
        input: "./src/components/index.rollup.ts",
        output: [
            {
                file: "./dist/leptonjs.js",
                format: "esm",
                sourcemap: true
            },
            {
                file: "./dist/leptonjs.min.js",
                format: "esm",
                sourcemap: true,
                plugins: [terser()],
            },
        ],
        plugins: [typescript({
            sourceMap: true,
            tsconfig: "./tsconfig.json",
            inlineSources: true
        })],
    },
    {
        // path to your declaration files root
        input: './dist/dts/index.rollup.d.ts',
        output: [{
            file: './dist/leptonjs.d.ts',
            format: 'es',
            sourcemap: true
        }],
        plugins: [dts()],
    }
];
