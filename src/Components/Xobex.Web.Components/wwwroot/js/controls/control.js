export class Control {
    constructor(element, properties) {
        this._element = element;
    }
    get element() {
        return this._element;
    }
}
export class ControlWrapper extends Control {
    constructor(element, properties) {
        super(element, properties);
        this._innerControl = this.createInnerControl(properties);
    }
    get innerControl() {
        return this._innerControl;
    }
}
//# sourceMappingURL=control.js.map