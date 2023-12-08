const CheckBoxDefaultProperties: DevExpress.ui.dxCheckBox.Properties = {

}

export class CheckBox extends HTMLElement {
    private _imnerControl!: DevExpress.ui.dxCheckBox;

    constructor() {
        super();
    }

    public connectedCallback() {
        const propertiesScript = this.querySelector("script[data-properties]");
        let properties: DevExpress.ui.dxCheckBox.Properties;
        if (propertiesScript?.textContent) {
            properties = {
                ...CheckBoxDefaultProperties,
                ...JSON.parse(propertiesScript.textContent)
            };
        }
        else {
            properties = {
                ...CheckBoxDefaultProperties
            }
        }
        this._imnerControl = this.createInnerControl(properties);
    }

    public get innerControl(): DevExpress.ui.dxCheckBox {
        return this._imnerControl;
    }

    protected createInnerControl(properties: DevExpress.ui.dxCheckBox.Properties): DevExpress.ui.dxCheckBox {
        return new DevExpress.ui.dxCheckBox(this, properties);
    }

    public get properties(): DevExpress.ui.dxCheckBox.Properties {
        return this.innerControl.option();
    }

    public set properties(value: DevExpress.ui.dxCheckBox.Properties) {
        this.innerControl.option(value);
    }

    public set disabled(value: boolean) {
        this.innerControl.option("disabled", value);
    }

    public get disabled(): boolean {
        return this.innerControl.option("disabled") as boolean;
    }

    public get text(): string | null {
        return this.innerControl.option("text") as string;
    }
    public set text(value: string | null) {
        this.innerControl.option("text", value ?? "");
    }
}

customElements.define("x-check-box", CheckBox);
