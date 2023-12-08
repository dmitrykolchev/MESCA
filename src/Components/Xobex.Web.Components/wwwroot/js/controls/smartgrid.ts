export class SmartGrid extends HTMLElement {
    private _resizeObserver: ResizeObserver;
    private _columns: GridColumnCollection;    
    private _fitColumns: boolean = true;
    private _header?: GridHeaderElement;

    constructor() {
        super();
        this._resizeObserver = new ResizeObserver(this.handleResize.bind(this));
        this._columns = new GridColumnCollection(this);
    }

    public get fitColumns(): boolean {
        return this._fitColumns;
    }

    public connectedCallback() {
        this._resizeObserver.observe(this);
    }

    public get columns(): GridColumnCollection {
        return this._columns;
    }

    public render(): void {
        this._header = new GridHeaderElement();
        this.appendChild(this._header);
        for (let i = 0; i < this.columns.length; ++i) {
            const column = this.columns.at(i);
            const element = new GridColumnHeaderElement(column);
            this._header?.appendChild(element);
        }
    }

    public invalidateControl(entry: ResizeObserverEntry): void {
        const boundingRect = this.getBoundingClientRect()
        console.debug(`control resized ${entry.contentRect.width} ${boundingRect.width}`);
    }

    private handleResize(entries: ResizeObserverEntry[], observer: ResizeObserver) {
        for (const entry of entries) {
            if (entry.target === this) {
                this.invalidateControl(entry);
            }
        }
    }
}

export interface IGridColumnProperties {
    dataField?: string;
    caption?: string;
    width?: number;
    minWidth?: number;
    maxWidth?: number;
    scaleable?: boolean;
    visible?: boolean;
    resizable?: boolean;
}

export class GridColumnCollection {
    private _owner: SmartGrid;
    private _columns: GridColumn[];

    constructor(owner: SmartGrid) {
        this._owner = owner;
        this._columns = [];
    }

    public add(item: GridColumn): void {
        this._columns.push(item);
        item.parent = this._owner;
    }

    public removeAt(index: number): boolean {
        return this._columns.splice(index, 1).length > 0;
    }

    public get length(): number {
        return this._columns.length;
    }

    public at(index: number): GridColumn {
        return this._columns[index];
    }
}

export class GridColumn {
    private _parent?: SmartGrid;
    private _dataField?: string;
    private _caption?: string;
    private _width?: number;
    private _minWidth?: number;
    private _maxWidth?: number;
    private _scaleable: boolean;
    private _visible: boolean;
    private _resizable: boolean;

    constructor(properties: IGridColumnProperties) {
        this._dataField = properties.dataField;
        this._caption = properties.caption;
        this._width = properties.width ?? 96;
        this._minWidth = properties.minWidth ?? 8;
        this._maxWidth = properties.maxWidth;
        this._scaleable = properties.scaleable ?? true;
        this._visible = properties.visible ?? true;
        this._resizable = properties.resizable ?? true;
    }

    public get dataField(): string | undefined {
        return this._dataField;
    }

    public get caption(): string | undefined {
        return this._caption;
    }

    public get width(): number | undefined {
        return this._width;
    }

    public get minWidth(): number | undefined {
        return this._minWidth;
    }

    public get maxWidth(): number | undefined {
        return this._maxWidth;
    }

    public get scaleable(): boolean {
        return this._scaleable;
    }

    public get visible(): boolean {
        return this._visible;
    }

    public get resiable(): boolean {
        return this._resizable;
    }
    public get parent(): SmartGrid | undefined {
        return this._parent;
    }

    public set parent(value: SmartGrid | undefined) {
        this._parent = value;
    }
}

abstract class GridElement extends HTMLElement {
    constructor() {
        super();
    }
}

export class GridHeaderElement extends GridElement {
    constructor() {
        super();
    }
    public connectedCallback() {
    }
}

export class GridColumnHeaderElement extends GridElement {
    private _column: GridColumn;
    constructor(column: GridColumn) {
        super();
        this._column = column;
    }

    public connectedCallback() {
        this.innerHTML = `<div>${this._column.caption}</div>`;
        this.style.cssText = `width:${this._column.width}px;`;
    }

    public get column(): GridColumn {
        return this._column;
    }
}

customElements.define("x-smart-grid", SmartGrid);
customElements.define("x-header", GridHeaderElement);
customElements.define("x-column-header", GridColumnHeaderElement);

