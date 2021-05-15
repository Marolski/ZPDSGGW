export default class DateHelper{
    cutDate(date: string){
        date = date.replace('T',' ');
        date = date.substring(0, 16)
        return date
    }
}