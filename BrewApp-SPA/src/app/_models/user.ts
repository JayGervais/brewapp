import { Photo } from './photo';

export interface User {
    id: number;
    username: string;
    firstname: string;
    lastname: string;
    address?: string;
    city: string;
    prov: string;
    country: string;
    description?: string;
    created: Date;
    photoUrl: string;
    photos?: Photo[];
}
