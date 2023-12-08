export class InvalidOperationException extends Error {
    constructor(message?: string) {
        super(message);
    }
}

export class NotImplementedException extends Error {
    constructor(message?: string) {
        super(message);
    }
}

export class ArgumentNullException extends Error {
    private _parameterName?: string;

    constructor(parameterName?: string, message?: string) {
        super(message);
        this._parameterName = parameterName;
    }

    public get parameterName(): string | undefined {
        return this._parameterName;
    }
}
