import IModel from '../../types/File';
export default class PathHelepr{
    getPathList(fileList: Array<IModel>){
        const fileDict: object[] = [];
        fileList.forEach(element => {
            let name = '';
            let kind ='';
            let specialChar = false;
            let nameStrictly ='';
            name = element.Path.slice(53);  
            for (let index = 0; index < name.length; index++) {
                if(name[index]!="\\" && specialChar==false)
                    kind += name[index];
                else{
                    specialChar = true;
                    nameStrictly +=name[index];
                }
            }
            nameStrictly = nameStrictly.substring(1);
            fileDict.push({
                key: nameStrictly,
                value: element,
                kind: kind
            })        
        });
        return fileDict;
    }
}