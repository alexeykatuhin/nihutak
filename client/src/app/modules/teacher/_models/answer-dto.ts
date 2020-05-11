export class AnswerDto {
    constructor(public id: number,public answer :string= null, public status: string ="",
        public errorMessage: string =""){}

}