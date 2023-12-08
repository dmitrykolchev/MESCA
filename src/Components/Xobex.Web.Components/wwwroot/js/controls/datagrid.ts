import { HttpClient } from "../net/httpclient.js";

const DataGridDefaultProperties: DevExpress.ui.dxDataGrid.Properties = {
    showBorders: true,
    allowColumnReordering: true,
    allowColumnResizing: true,
    rowAlternationEnabled: false,
    showRowLines: true,
    showColumnLines: true,
    columnResizingMode: "widget",
    paging: { enabled: false },
    selection: { mode: "single" },
    sorting: { mode: "multiple" },
    headerFilter: { visible: true },
    filterRow: { visible: false },
    searchPanel: { visible: false },
    loadPanel: { enabled: false },
    scrolling: { mode: "virtual", showScrollbar: "always", useNative: "auto" },
};

export class DataGrid extends HTMLElement {
    private _imnerControl!: DevExpress.ui.dxDataGrid;

    constructor() {
        super();
    }

    public connectedCallback() {
        const propertiesScript = this.querySelector("script[data-properties]");
        let properties: DevExpress.ui.dxDataGrid.Properties;
        if (propertiesScript?.textContent) {
            properties = {
                ...DataGridDefaultProperties,
                ...JSON.parse(propertiesScript.textContent)
            };
        }
        else {
            properties = { ...DataGridDefaultProperties }
        }
        this._imnerControl = this.createInnerControl(properties);
    }

    public get innerControl(): DevExpress.ui.dxDataGrid {
        return this._imnerControl;
    }

    protected createInnerControl(properties: DevExpress.ui.dxDataGrid.Properties): DevExpress.ui.dxDataGrid {
        return new DevExpress.ui.dxDataGrid(this, properties);
    }

    public get properties(): DevExpress.ui.dxDataGrid.Properties {
        return this.innerControl.option();
    }

    public set properties(value: DevExpress.ui.dxDataGrid.Properties) {
        this.innerControl.option(value);
    }

    public set disabled(value: boolean) {
        this.innerControl.option("disabled", value);
    }

    public get disabled(): boolean {
        return this.innerControl.option("disabled") as boolean;
    }

    public get columns(): DevExpress.ui.dxDataGrid.Column[] {
        return this.innerControl.option("columns") as DevExpress.ui.dxDataGrid.Column[];
    }

    public get dataSource(): unknown {
        return this.innerControl.option("dataSource");
    }

    public set dataSource(value: unknown) {
        if (typeof value === "object") {
            this.innerControl.option("dataSource", value);
        }
        else if (typeof value === "function") {
            this.innerControl.option("dataSource", value());
        }
        else if (typeof value === "string") {
            (async () => {
                const client = new HttpClient();
                const data = await client.get(value).send()
                this.innerControl.option("dataSource", data.response);
            })();
        }
        else {
            throw new Error("unsupported datasource type");
        }
    }
}

customElements.define("x-data-grid", DataGrid);
