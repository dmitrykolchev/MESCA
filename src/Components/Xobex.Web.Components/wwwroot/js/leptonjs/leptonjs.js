class InvalidOperationException extends Error {
    constructor(message) {
        super(message);
    }
}
class NotImplementedException extends Error {
    constructor(message) {
        super(message);
    }
}
class ArgumentNullException extends Error {
    constructor(parameterName, message) {
        super(message);
        this._parameterName = parameterName;
    }
    get parameterName() {
        return this._parameterName;
    }
}

class Random {
    static next(min, max) {
        return Math.floor(Math.random() * (max - min + 1)) + min;
    }
}

export { ArgumentNullException, InvalidOperationException, NotImplementedException, Random };
//# sourceMappingURL=leptonjs.js.map
