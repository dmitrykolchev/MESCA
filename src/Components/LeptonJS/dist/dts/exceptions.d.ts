export declare class InvalidOperationException extends Error {
    constructor(message?: string);
}
export declare class NotImplementedException extends Error {
    constructor(message?: string);
}
export declare class ArgumentNullException extends Error {
    private _parameterName?;
    constructor(parameterName?: string, message?: string);
    get parameterName(): string | undefined;
}
