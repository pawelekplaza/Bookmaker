import { LoginType } from './loginType';

export function LoginTypeDecorator(constructor: Function) {
    constructor.prototype.LoginType = LoginType;
}