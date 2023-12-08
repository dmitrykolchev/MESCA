const IconSizeValue = {
    "xxs": 12,
    "xs": 16,
    "s": 20,
    "n": 24,
    "m": 32,
    "l": 40,
    "xl": 48,
    "xxl": 64
};
export class IconBase extends HTMLElement {
    constructor() {
        super();
    }
    static getFontSize(iconSize) {
        return iconSize ? (IconSizeValue[iconSize] ?? IconSizeValue["s"]) : IconSizeValue["s"];
    }
    createInner(shadow) {
        const size = IconBase.getFontSize(this.getAttribute("size"));
        const weight = this.getAttribute("weight") ?? "300";
        const fill = this.getAttribute("fill") ?? "0";
        const grad = this.getAttribute("grad") ?? "0";
        const fontVariations = `'opsz' ${size}, 'wght' ${weight}, 'FILL' ${fill}, 'GRAD' ${grad}`;
        const fontWeight = this.getAttribute("font-weight") ?? "normal";
        this._styleElement = document.createElement("style");
        this._styleElement.textContent = `
:host {
    min-width: ${size}px;
    min-height: ${size}px;
    max-width: ${size}px;
    max-height: ${size}px;
    display: inline-block;
}
:host:before {
    content: '${this.getGlyph()}';
    font-family: ${this.getFontFamily()};
    font-size: ${size}px;
    font-weight: ${fontWeight};
    font-variation-settings: ${fontVariations};
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    font-style: normal;
    font-variant: normal;
    line-height: 1;
    speak: none;
    font-optical-sizing: auto;
}
`;
        shadow.appendChild(this._styleElement);
    }
}
//# sourceMappingURL=icon.js.map