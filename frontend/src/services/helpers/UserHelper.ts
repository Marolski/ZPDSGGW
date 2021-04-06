import UserService from '../UserService';
import IUser from "../../types/User";

const userService = new UserService();
export default class UserHelper{
    getUserName(user){
        if(user.Role == 'Promoter')
            return `${user.Degrees} ${user.Name} ${user.Surname}`
        else return `${user.Name} ${user.Surname}`
    }
}