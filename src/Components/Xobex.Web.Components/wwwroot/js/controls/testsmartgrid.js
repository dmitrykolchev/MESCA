import { GridColumn } from "./smartgrid.js";
export function initialize() {
    const instance = document.getElementById("grid");
    instance.columns.add(new GridColumn({
        caption: "Column 1",
        width: 32,
        minWidth: 32,
        maxWidth: 32,
        scaleable: false
    }));
    instance.columns.add(new GridColumn({
        caption: "Column 2",
        width: 100
    }));
    instance.columns.add(new GridColumn({
        caption: "Column 3",
        width: 200
    }));
    instance.render();
}
//# sourceMappingURL=testsmartgrid.js.map