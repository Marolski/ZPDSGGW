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
    praca =1,
    wniosek,
    paymentConfirmation,
    Inne
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