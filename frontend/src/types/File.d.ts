import {DocumentKind} from "../enums/Enum";

export default interface IFile{
    Id: string,
    Path: string,
    DocumentKind: DocumentKind,
    UserId: string,
    Accepted: boolean,
    FileName: string
}