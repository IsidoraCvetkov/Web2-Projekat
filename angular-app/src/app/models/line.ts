import { Station } from "../models/station";

export class Line{
    Number: string;
    IdLine: number;
    Stations: Station[];
    RouteType: number;
}