module RaffleJS.Models {
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
        constructor(private name: string, public email: string) {
        }
    }

    export class RegularAttendee extends Attendee {
        getPrizeMessage(prize: Prize): string {
        	if (prize.name === 'Platic Water Bottle with Conference Logo') {
                return 'Tough luck, ' + this.name + '! You won another ' + prize.name;
            } else {
                return super.getPrizeMessage(prize);
            }
        }
        constructor(private name: string, email: string, public favoriteBeer?: string) {
            super(name, email);
        }
    }

    export class Organizer extends RegularAttendee {
        getPrizeMessage(prize: Prize): string {
        	if (prize.name === 'Golden xBox 720 Ultimate RT Pro 8 Series - 64 bit') {
                return 'WTF?, ' + this.name + ', you won a ' + prize.name + '! This seems suspect.';
            } else {
                return super.getPrizeMessage(prize);
            }
        }
        constructor(private name: string, email: string, favoriteBeer?: string) {
            super(name, email, favoriteBeer);
        }
    }

}