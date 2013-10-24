
export class Prize {
    name: string;

    constructor(name: string) {
        this.name = name;
    }
}

export interface IAttendee {
    getPrizeMessage(prize: Prize): string;
}

export class Attendee implements IAttendee {
    getPrizeMessage(prize: Prize): string {
        return 'Congrats, ' + this.name + '! You won a ' + prize.name;
    }
    constructor(public name: string, private email: string) {
    }
}

export class RegularAttendee extends Attendee {
    getPrizeMessage(prize: Prize): string {
        if (prize.name === 'Succeeding with Agile book') {
            return 'Tough luck, ' + this.name + '! You won another copy of ' + prize.name;
        } else {
            return super.getPrizeMessage(prize);
        }
    }
	constructor(name: string, email: string, public favoriteBeer?: string) {
        super(name, email);
    }
}

export class Organizer extends RegularAttendee {
    getPrizeMessage(prize: Prize): string {
        if (prize.name === 'KCDC Ticket') {
            return 'WTF?, ' + this.name + ', you won a ' + prize.name + '! This seems suspect.';
        } else {
            return super.getPrizeMessage(prize);
        }
    }
    constructor(name: string, email: string, favoriteBeer?: string) {
        super(name, email, favoriteBeer);
    }
}


