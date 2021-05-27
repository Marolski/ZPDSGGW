import UserService from '../UserService';
import {degrees} from '../../enums/Enum'

const userService = new UserService();
export default class UserHelper{
    getUserName(user){
        if(user.Role == 'Promoter')
            return `${degrees[user.Degrees]} ${user.Name} ${user.Surname}`
        else return `${user.Name} ${user.Surname}`
    }
}