const CheckBoxDefaultProperties = {};
export class CheckBox extends HTMLElement {
    constructor() {
        super();
    }
    connectedCallback() {
        const propertiesScript = this.querySelector("script[data-properties]");
        let properties;
        if (propertiesScript?.textContent) {
            properties = {
                ...CheckBoxDefaultProperties,
                ...JSON.parse(propertiesScript.textContent)
            };
        }
        else {
            properties = {
                ...CheckBoxDefaultProperties
            };
        }
        this._imnerControl = this.createInnerControl(properties);
    }
    get innerControl() {
        return this._imnerControl;
    }
    createInnerControl(properties) {
        return new DevExpress.ui.dxCheckBox(this, properties);
    }
    get properties() {
        return this.innerControl.option();
    }
    set properties(value) {
        this.innerControl.option(value);
    }
    set disabled(value) {
        this.innerControl.option("disabled", value);
    }
    get disabled() {
        return this.innerControl.option("disabled");
    }
    get text() {
        return this.innerControl.option("text");
    }
    set text(value) {
        this.innerControl.option("text", value ?? "");
    }
}
customElements.define("x-check-box", CheckBox);
//# sourceMappingURL=checkbox.js.map