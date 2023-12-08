export class SmartGrid extends HTMLElement {
    constructor() {
        super();
        this._fitColumns = true;
        this._resizeObserver = new ResizeObserver(this.handleResize.bind(this));
        this._columns = new GridColumnCollection(this);
    }
    get fitColumns() {
        return this._fitColumns;
    }
    connectedCallback() {
        this._resizeObserver.observe(this);
    }
    get columns() {
        return this._columns;
    }
    render() {
        this._header = new GridHeaderElement();
        this.appendChild(this._header);
        for (let i = 0; i < this.columns.length; ++i) {
            const column = this.columns.at(i);
            const element = new GridColumnHeaderElement(column);
            this._header?.appendChild(element);
        }
    }
    invalidateControl(entry) {
        const boundingRect = this.getBoundingClientRect();
        console.debug(`control resized ${entry.contentRect.width} ${boundingRect.width}`);
    }
    handleResize(entries, observer) {
        for (const entry of entries) {
            if (entry.target === this) {
                this.invalidateControl(entry);
            }
        }
    }
}
export class GridColumnCollection {
    constructor(owner) {
        this._owner = owner;
        this._columns = [];
    }
    add(item) {
        this._columns.push(item);
        item.parent = this._owner;
    }
    removeAt(index) {
        return this._columns.splice(index, 1).length > 0;
    }
    get length() {
        return this._columns.length;
    }
    at(index) {
        return this._columns[index];
    }
}
export class GridColumn {
    constructor(properties) {
        this._dataField = properties.dataField;
        this._caption = properties.caption;
        this._width = properties.width ?? 96;
        this._minWidth = properties.minWidth ?? 8;
        this._maxWidth = properties.maxWidth;
        this._scaleable = properties.scaleable ?? true;
        this._visible = properties.visible ?? true;
        this._resizable = properties.resizable ?? true;
    }
    get dataField() {
        return this._dataField;
    }
    get caption() {
        return this._caption;
    }
    get width() {
        return this._width;
    }
    get minWidth() {
        return this._minWidth;
    }
    get maxWidth() {
        return this._maxWidth;
    }
    get scaleable() {
        return this._scaleable;
    }
    get visible() {
        return this._visible;
    }
    get resiable() {
        return this._resizable;
    }
    get parent() {
        return this._parent;
    }
    set parent(value) {
        this._parent = value;
    }
}
class GridElement extends HTMLElement {
    constructor() {
        super();
    }
}
export class GridHeaderElement extends GridElement {
    constructor() {
        super();
    }
    connectedCallback() {
    }
}
export class GridColumnHeaderElement extends GridElement {
    constructor(column) {
        super();
        this._column = column;
    }
    connectedCallback() {
        this.innerHTML = `<div>${this._column.caption}</div>`;
        this.style.cssText = `width:${this._column.width}px;`;
    }
    get column() {
        return this._column;
    }
}
customElements.define("x-smart-grid", SmartGrid);
customElements.define("x-header", GridHeaderElement);
customElements.define("x-column-header", GridColumnHeaderElement);
//# sourceMappingURL=smartgrid.js.map