export class Control<TProperties>
{
    private _element: HTMLElement;

    constructor(element: HTMLElement, properties: TProperties) {
        this._element = element;
    }

    public get element(): HTMLElement {
        return this._element;
    }

}

export abstract class ControlWrapper<TInnerControl, TProperties> extends Control<TProperties>
{
    private _innerControl: TInnerControl;
    constructor(element: HTMLElement, properties: TProperties) {
        super(element, properties);
        this._innerControl = this.createInnerControl(properties);
    }

    public get innerControl(): TInnerControl {
        return this._innerControl;
    }

    protected abstract createInnerControl(properties: TProperties): TInnerControl;
}
