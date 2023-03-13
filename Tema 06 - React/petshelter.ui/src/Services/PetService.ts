import { Person } from "../Models/Person";
import { AnimalType, Pet } from "../Models/Pet";

export class PetService {

    pets: Array<Pet> = [
        new Pet(1, "Thor",
            "data:image/jpeg;base64,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",
            AnimalType.Cat,
            "Thor is a beast, Thor is a sweetheart. The best thing in the universe is a cardboard box. Scratch the box bite plants eat my own ears, kitty scratches couch bad kitty lick yarn hanging out of own butt. Attack curtains be a nyan cat, feel great about it, be annoying 24/7 poop rainbows in litter box all day bite the neighbor's bratty kid knock dish off table head butt cant eat out of my own dish, cough hairball, eat toilet paper but cough in the middle of the night i crawl onto your chest and purr gently to help you sleep. Touch my tail, i shred your hand purrrr. Thug cat hunt anything that moves, but proudly present butt to human run up and down stairs pet me pet me don't pet me for bite nose of your human. ",
            new Date(2022, 3, 22),
            true,
            5,
            true,
            new Person(1, "Elena", "0755777444"),
            new Person(1, "Elena", "0755777444")
        ),
        new Pet(2, "Marco",
            "https://s01.sgp1.cdn.digitaloceanspaces.com/article/51036-cwobnirfka-1580816618.jpeg",
            AnimalType.Cat,
            "Marco is a lovely kitten. The best thing in the universe is a cardboard box. Scratch the box bite plants eat my own ears, kitty scratches couch bad kitty lick yarn hanging out of own butt. Attack curtains be a nyan cat, feel great about it, be annoying 24/7 poop rainbows in litter box all day bite the neighbor's bratty kid knock dish off table head butt cant eat out of my own dish, cough hairball, eat toilet paper but cough in the middle of the night i crawl onto your chest and purr gently to help you sleep. Touch my tail, i shred your hand purrrr. Thug cat hunt anything that moves, but proudly present butt to human run up and down stairs pet me pet me don't pet me for bite nose of your human. ",
            new Date(2022, 3, 22),
            true,
            4,
            true,
            new Person(1, "Elena", "0755777444"),
        ),
        new Pet(3, "Isis",
            "data:image/jpeg;base64,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",
            AnimalType.Cat,
            "Marco is a lovely kitten. The best thing in the universe is a cardboard box. Scratch the box bite plants eat my own ears, kitty scratches couch bad kitty lick yarn hanging out of own butt. Attack curtains be a nyan cat, feel great about it, be annoying 24/7 poop rainbows in litter box all day bite the neighbor's bratty kid knock dish off table head butt cant eat out of my own dish, cough hairball, eat toilet paper but cough in the middle of the night i crawl onto your chest and purr gently to help you sleep. Touch my tail, i shred your hand purrrr. Thug cat hunt anything that moves, but proudly present butt to human run up and down stairs pet me pet me don't pet me for bite nose of your human. ",
            new Date(2022, 3, 22),
            true,
            4.5,
            true,
            new Person(1, "Elena", "0755777444")
        )
    ]


    public getAll(): Pet[] {
        return this.pets;
    }

    add(pet: Pet) {
        this.pets.push(pet);
    }

    update(pet: Pet) {
        this.pets = this.pets.filter(p => p.name !== pet.name);
        this.pets.push(pet);
    }

    delete(pet: Pet) {
        this.pets = this.pets.filter(p => p.name !== pet.name);
    }

}