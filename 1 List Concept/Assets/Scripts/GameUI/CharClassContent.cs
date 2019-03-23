using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharClassContent : MonoBehaviour {

	public static string[] classTexts = {
		"CLOUD MASTER",
		"NOVIST",
		"SEER OF WAR",
		"SHAPE SHIFTER"
	};

	public static float[] charHPTexts = {
		260.0f,
		380.0f,
		300.0f,
		220.0f
	};

	public static float[] charDEFTexts = {
		8.0f,
		18.0f,
		10.0f,
		6.0f
	};

	public static float[] charMSPDTexts = {
		10.0f,
		8.0f,
		9.0f,
		11.0f
	};

	public static string[] attackOneTitleTexts = {
		"Wrench Punch",
		"Meteor Shot",
		"Card Throw",
		"Double Hit"
	};

	public static string[] attackTwoTitleTexts = {
		"Bomb Throw",
		"Fire Block",
		"Ground Shake",
		"???"
	};

	public static string[] skillTitles = {
		"Turret Gun",
		"Companion",
		"Crystal Ball",
		"Shapeshift"
	};

	public static string[] enableSkillTexts = {
        "Shoots enemies on sight",
        "Attacks enemies on sight",
        "Blinds nearby enemies",
        "Fight faster, move quicker"
    };

	public static string[] improveSkillTexts = {
        "Attack Speed +5%, Radius +4m",
        "Radius +3m, Lifetime +4s",
        "Radius +2m, Blinding Duration +1s",
        "Duration +3s, Attack Speed +10%"
    };

	public static string[] passiveTitles = {
		"Self Repair",
		"Slowing Tendrils",
		"Foresight",
		"Invisibility Cloak"
	};

	public static string[] passiveSkillTexts = {
        "Auto-heal over time",
        "Slows nearby enemies",
        "See enemy's locations",
        "Decreases visibility"
    };


	public Sprite[] classImages = {};
	public Sprite[] attackOneImages = {};
	public Sprite[] attackTwoImages = {};


	public static string[] titleTexts = {
		"Mr.",
		"Mrs.",
		"Miss",
		"Dr.",
		"Monsieur",
		"Madame",
		"Prof.",
		"El",
		"La",
		"Das",
		"The",
		"That",
		"Lord",
		"Lady",
		"Master",
		"Sir",
		"President",
		"Captain",
		"Major",
		"Prince",
		"Princess",
		"King",
		"Queen",
		"A",
		"Kaiser",
		"Duke",
		"Pope",
		"Priest",
		"Priestess",
		"Pretty",
		"The last",
		"One",
		"No"
	};

	public static string[] adjectiveTexts = {
		"Black",
		"White",
		"Red",
		"Green",
		"Yellow",
		"Blue",
		"Dark",
		"Pink",
		"Purple",
		"Self-Conscious",
		"Concerned",
		"Pretty",
		"Ugly",
		"Awesome",
		"Sexy",
		"Awful",
		"Random",
		"Stupid",
		"Creepy",
		"Nice",
		"Silly",
		"Stoned",
		"Fluffy",
		"Hard",
		"Mighty",
		"Great",
		"Big",
		"Smart",
		"Strong",
		"Hairy",
		"Stinky",
		"Dirty",
		"Tiny",
		"Naked",
		"Sweaty",
		"Small",
		"Grumpy",
		"Fantastic",
		"Extraordinary",
		"Good",
		"High",
		"Fancy",
		"Crazy",
		"Lazy",
		"Dancing",
		"Proper",
		"Creative",
		"Fat",
		"Chubby",
		"Shaved",
		"Unattractive",
		"Attractive",
		"Smelly",
		"Touchy",
		"Colorful",
		"Drunken",
		"Sticky",
		"Handsome",
		"Disgusting",
		"Ridiculous",
		"Old",
		"Young",
		"Racist",
		"Tall",
		"Wasted",
		"Introverted",
		"Fast",
		"Slow",
		"Filthy",
		"Dramatic",
		"Sick",
		"Tasty",
		"Brave",
		"Funny",
		"Wild",
		"Epic",
		"Sneaky",
		"Fresh",
		"Golden",
		"Wet",
		"Busy",
		"New"
	};

	public static string[] nameTexts = {
		"Jimmy Changa",
		"Mojo",
		"Dude",
		"Dudess",
		"Douchebag",
		"Astronaut",
		"Cactus",
		"Banana",
		"Strawberry",
		"Onion",
		"Pineapple",
		"Ananas",
		"Taco",
		"Pizza",
		"Teddy Bear",
		"Chicken",
		"Sheep",
		"Koala",
		"Monkey",
		"Horse",
		"Unicorn",
		"Squirrel",
		"Grandma",
		"Grandpa",
		"Pug",
		"Elephant",
		"Hamster",
		"Raccoon",
		"Goat",
		"Goldfish",
		"Pylonus",
		"Knight",
		"Johnny",
		"Jimbo",
		"Johnson",
		"Quentin",
		"Sally",
		"Mary Jane",
		"Jack",
		"Teabag",
		"Bo",
		"Username",
		"Butt",
		"Bellybutton",
		"Machine",
		"Sushi",
		"Avocado",
		"Hat",
		"Washing Machine",
		"Star",
		"Fart",
		"Baby",
		"Broccoli",
		"Savage",
		"Sauerkraut",
		"Pig",
		"Panda",
		"Jellyfish",
		"Llama",
		"Dog",
		"Yeti",
		"Peanut",
		"Soul",
		"Donut",
		"Firework",
		"Moon Man",
		"Mole Man",
		"Sloth",
		"Blobfish",
		"Beer",
		"Captain",
		"Sauerkraut",
		"God",
		"Godfather",
		"Racist",
		"Monster",
		"Boob",
		"Nipple",
		"Devil",
		"Owl",
		"Moustache",
		"Bacon",
		"Boy",
		"Girl",
		"Hero",
		"Designer",
		"Developer",
		"Nerd",
		"Samurai",
		"Ninja",
		"Mage",
		"Warrior",
		"Adventurer",
		"Dwarf",
		"Human",
		"Elf",
		"Orc",
		"Drunk",
		"Ghost",
		"Friend",
		"Egg",
		"Beast",
		"Beaver",
		"Eagle",
		"Fellow",
		"Pancake",
		"Cookie",
		"Squid"
	};

}
