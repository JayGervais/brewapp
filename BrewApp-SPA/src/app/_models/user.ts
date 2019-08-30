import { Photo } from './photo';

export interface User {
    user_Id: number;
    username: string;
    firstname: string;
    lastname: string;
    address?: string;
    city: string;
    prov: string;
    country: string;
    description?: string;
    created: Date;
   // lastActive: Date;
    photoUrl: string;
    photos?: Photo[];
}
