"use strict";
const themeAccent = [
    {
        "name": "yellowDark",
        "hex": "#d29200",
        "code": {
            "core": "$ms-color-yellowDark",
            "themeSlot": "yellowDark"
        }
    },
    {
        "name": "yellow",
        "hex": "#ffb900",
        "code": {
            "core": "$ms-color-yellow",
            "themeSlot": "yellow"
        }
    },
    {
        "name": "yellowLight",
        "hex": "#fff100",
        "code": {
            "core": "$ms-color-yellowLight",
            "themeSlot": "yellowLight"
        }
    },
    {
        "name": "orange",
        "hex": "#d83b01",
        "code": {
            "core": "$ms-color-orange",
            "themeSlot": "orange"
        }
    },
    {
        "name": "orangeLight",
        "hex": "#ea4300",
        "code": {
            "core": "$ms-color-orangeLight",
            "themeSlot": "orangeLight"
        }
    },
    {
        "name": "orangeLighter",
        "hex": "#ff8c00",
        "code": {
            "core": "$ms-color-orangeLighter",
            "themeSlot": "orangeLighter"
        }
    },
    {
        "name": "redDark",
        "hex": "#a4262c",
        "code": {
            "core": "$ms-color-redDark",
            "themeSlot": "redDark"
        }
    },
    {
        "name": "red",
        "hex": "#d13438",
        "code": {
            "core": "$ms-color-red",
            "themeSlot": "red"
        }
    },
    {
        "name": "magentaDark",
        "hex": "#5c005c",
        "code": {
            "core": "$ms-color-magentaDark",
            "themeSlot": "magentaDark"
        }
    },
    {
        "name": "magenta",
        "hex": "#b4009e",
        "code": {
            "core": "$ms-color-magenta",
            "themeSlot": "magenta"
        }
    },
    {
        "name": "magentaLight",
        "hex": "#e3008c",
        "code": {
            "core": "$ms-color-magentaLight",
            "themeSlot": "magentaLight"
        }
    },
    {
        "name": "purpleDark",
        "hex": "#32145a",
        "code": {
            "core": "$ms-color-purpleDark",
            "themeSlot": "purpleDark"
        }
    },
    {
        "name": "purple",
        "hex": "#5c2d91",
        "code": {
            "core": "$ms-color-purple",
            "themeSlot": "purple"
        }
    },
    {
        "name": "purpleLight",
        "hex": "#b4a0ff",
        "code": {
            "core": "$ms-color-purpleLight",
            "themeSlot": "purpleLight"
        }
    },
    {
        "name": "blueDark",
        "hex": "#002050",
        "code": {
            "core": "$ms-color-blueDark",
            "themeSlot": "blueDark"
        }
    },
    {
        "name": "blueMid",
        "hex": "#00188f",
        "code": {
            "core": "$ms-color-blueMid",
            "themeSlot": "blueMid"
        }
    },
    {
        "name": "blue",
        "hex": "#0078d4",
        "code": {
            "core": "$ms-color-blue",
            "themeSlot": "blue"
        }
    },
    {
        "name": "blueLight",
        "hex": "#00bcf2",
        "code": {
            "core": "$ms-color-blueLight",
            "themeSlot": "blueLight"
        }
    },
    {
        "name": "tealDark",
        "hex": "#004b50",
        "code": {
            "core": "$ms-color-tealDark",
            "themeSlot": "tealDark"
        }
    },
    {
        "name": "teal",
        "hex": "#008272",
        "code": {
            "core": "$ms-color-teal",
            "themeSlot": "teal"
        }
    },
    {
        "name": "tealLight",
        "hex": "#00B294",
        "code": {
            "core": "$ms-color-tealLight",
            "themeSlot": "tealLight"
        }
    },
    {
        "name": "greenDark",
        "hex": "#004b1c",
        "code": {
            "core": "$ms-color-greenDark",
            "themeSlot": "greenDark"
        }
    },
    {
        "name": "green",
        "hex": "#107c10",
        "code": {
            "core": "$ms-color-green",
            "themeSlot": "green"
        }
    },
    {
        "name": "greenLight",
        "hex": "#bad80a",
        "code": {
            "core": "$ms-color-greenLight",
            "themeSlot": "greenLight"
        }
    }
];
for (const item of themeAccent) {
    console.log(`${item.code.core}: ${item.hex};`);
}
//# sourceMappingURL=app.js.map