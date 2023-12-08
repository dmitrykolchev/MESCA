import { HttpClient } from "../net/httpclient.js";
const DataGridDefaultProperties = {
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
    constructor() {
        super();
    }
    connectedCallback() {
        const propertiesScript = this.querySelector("script[data-properties]");
        let properties;
        if (propertiesScript?.textContent) {
            properties = {
                ...DataGridDefaultProperties,
                ...JSON.parse(propertiesScript.textContent)
            };
        }
        else {
            properties = { ...DataGridDefaultProperties };
        }
        this._imnerControl = this.createInnerControl(properties);
    }
    get innerControl() {
        return this._imnerControl;
    }
    createInnerControl(properties) {
        return new DevExpress.ui.dxDataGrid(this, properties);
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
    get columns() {
        return this.innerControl.option("columns");
    }
    get dataSource() {
        return this.innerControl.option("dataSource");
    }
    set dataSource(value) {
        if (typeof value === "object") {
            this.innerControl.option("dataSource", value);
        }
        else if (typeof value === "function") {
            this.innerControl.option("dataSource", value());
        }
        else if (typeof value === "string") {
            (async () => {
                const client = new HttpClient();
                const data = await client.get(value).send();
                this.innerControl.option("dataSource", data.response);
            })();
        }
        else {
            throw new Error("unsupported datasource type");
        }
    }
}
customElements.define("x-data-grid", DataGrid);
//# sourceMappingURL=datagrid.js.map