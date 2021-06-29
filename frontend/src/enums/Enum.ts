export enum InvitationStatus{
    InProgress = 1,
    Send,
    Accepted,
    Rejected
}

export enum ProposalStatus{
    InProgress = 1,
    Send,
    Delivered,
    Received,
    Reject,
    Accepted
}

export enum DocumentKind{
    thesis =1,
    proposal,
    paymentConfirmation,
    Inne
}

export const documentKind = {
    1: "Praca ",
    2: "Wniosek",
    3: "Potwierdzenie płatności",
    4: "Inne"
}

export enum Roles{
    Admin = 1,
    Student,
    Promoter,
    Deanery
}

export enum Degrees{
    inz =1,
    mgr,
    dr,
    prof,
    drInz,
    drMgr,
    drHab,
}

export enum ThesisTopicStatus{
    available =1,
    unavailable,
    reserved
}
export const invitationProposalStatus = {
    1: "W przygotowaniu", 
    2: "Wysłany", 
    3: "Zaakceptowany",
    4: "Odrzucony"
}



export const degrees = {
    1: "inż.",
    2: "mgr",
    3: "dr",
    4: "prof.",
    5: "dr inż.",
    6: "dr hab.",
    7: "dr hab. inż.",
    8: "mgr inż.",
    9: "dr hab. prof. nadzw. SGGW",
    10: "dr hab. prof. SGGW",
    11: "prof. dr hab.",
}

export const thesisTopicStatus = {
    1: "Dostępny",
    2: "Niedostępny",
    3: "Zarezerwowany",
}