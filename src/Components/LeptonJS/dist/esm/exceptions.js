export class InvalidOperationException extends Error {
    constructor(message) {
        super(message);
    }
}
export class NotImplementedException extends Error {
    constructor(message) {
        super(message);
    }
}
export class ArgumentNullException extends Error {
    constructor(parameterName, message) {
        super(message);
        this._parameterName = parameterName;
    }
    get parameterName() {
        return this._parameterName;
    }
}
//# sourceMappingURL=exceptions.js.map