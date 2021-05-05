import IModel from '../../types/File';
export default class PathHelepr{
    getPathList(fileList: Array<IModel>){
        let name = '';
        const fileDict: object[] = [];
        fileList.forEach(element => {
            name = element.Path.slice(53);  
            fileDict.push({
                key: name,
                value: element
            })        
        });
        console.log(fileDict)
        return fileDict;
    }
}