export interface IMatch {
    id: number;
    hostTeamId: number;
    hostTeamName: string;
    guestTeamId: number;
    guestTeamName: string;
    stadiumId: number;
    startTime: Date;
    resultId: number;
}