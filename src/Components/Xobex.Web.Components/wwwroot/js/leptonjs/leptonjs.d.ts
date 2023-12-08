declare class InvalidOperationException extends Error {
    constructor(message?: string);
}
declare class NotImplementedException extends Error {
    constructor(message?: string);
}
declare class ArgumentNullException extends Error {
    private _parameterName?;
    constructor(parameterName?: string, message?: string);
    get parameterName(): string | undefined;
}

declare class Random {
    static next(min: number, max: number): number;
}

export { ArgumentNullException, InvalidOperationException, NotImplementedException, Random };
//# sourceMappingURL=leptonjs.d.ts.map
