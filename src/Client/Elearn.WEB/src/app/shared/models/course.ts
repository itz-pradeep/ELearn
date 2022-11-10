export interface ICourse {
    id: string;
    name:string;
    description:string;
    hours:number;
    minutes:number;
    technology:string;
    launchUrl:string;
    createdDate: Date;
    isActive: boolean;
}