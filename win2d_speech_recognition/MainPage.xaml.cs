using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechRecognition;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace win2d_speech_recognition {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args) {
            switch (args.VirtualKey) {
                case Windows.System.VirtualKey.Space:
                    nIndex = (nIndex + 1) % Puzzles.Count;
                    break;
            }
        }

        List<PalindromePuzzle> Puzzles = new List<PalindromePuzzle>();
        int nIndex = 0;

        private async Task RecordSpeechFromMicrophoneAsync() {
            var speechRecognizer = new SpeechRecognizer();
            speechRecognizer.Timeouts.InitialSilenceTimeout = new TimeSpan(0, 1, 0);
                //UIOptions.IsReadBackEnabled = false;

            await speechRecognizer.CompileConstraintsAsync();
            SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeAsync();//RecognizeWithUIAsync();
            
            // str = speechRecognitionResult.Text;
            //var messageDialog = new Windows.UI.Popups.MessageDialog(speechRecognitionResult.Text, "Text spoken");
            //await messageDialog.ShowAsync();
        }

        private void canvasMain_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args) {
            Puzzles[nIndex].Draw(args);
        }

        private void canvasMain_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args) {
            Puzzles[nIndex].Update(args);
        }

        private void canvasMain_CreateResources(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args) {
            mediaSimple.MediaPlayer.RealTimePlayback = true;
            mediaSimple.MediaPlayer.IsLoopingEnabled = true;
            //Puzzles.Add(new PalindromePuzzle(sender.Device, "Sore was I ere I looked at Eros.", "saw"));
            //Puzzles.Add(new PalindromePuzzle(sender.Device, "Marge let a moody infant doom a telegram.", "baby"));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Sore was I ere I saw Eros.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "A man, a plan, a canal, Panama", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Never a foot too far, even.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Euston saw I was not Sue.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Live on evasions? No, I save no evil.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Red Roses run no risk, sir, on nurses order.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Salisbury moor, sir, is roomy. Rub Silas.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Marge, let's 'went.' I await news telegram.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "A new order began, a more Roman age bred Rowena.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "I, man, am regal; a German am I.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Tracy, no panic in a pony-cart.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Egad! Loretta has Adams as mad as a hatter. Old age!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Eve, mad Adam, Eve!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Resume so pacific a pose, muser.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Marge let a moody baby doom a telegram.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Tenet C is a basis, a basic tenet.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Nella's simple hymn: 'I attain my help, Miss Allen.'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Straw? No, too stupid a fad. I put soot on warts.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Sir, I demand, I am a maid named Iris.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Lay a wallaby baby ball away, Al.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Tessa's in Italy, Latin is asset.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Noel sees Leon.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No, it can assess an action.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Bob: 'Did Anna peep?' Anna: 'Did Bob?'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Sex at noon taxes.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Egad, a base life defiles a bad age.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Stop!' nine myriad murmur. 'Put up rum, rum, dairymen, in pots.'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Delia, here we nine were hailed.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Not I, no hotel, cycle to Honiton.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Anne, I vote more cars race Rome-to-Vienna.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Mother Eve's noose we soon sever, eh, Tom?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Sue,' Tom smiles, 'Selim smote us.'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Telegram, Margelet!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Too hot to hoot.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Unglad, I tar a tidal gnu.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Eve damned Eden, mad Eve.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Snug Satraps eye Sparta's guns.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Nurse, save rare vases, run!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Draw, O Caesar, erase a coward.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No mists or frost, Simon.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Sail on, game vassal! Lacy callas save magnolias!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Trap a rat! Stare, piper, at Star apart.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Sue, dice, do, to decide us.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "La, Mr. O'Neill, lie normal.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Top step -- Sara's pet spot.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Eel-fodder, stack-cats red do flee.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Reg, no lone car won, now race no longer.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Zeus was deified, saw Suez.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Evil is a name of a foeman, as I live.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No waste, grab a bar, get saw on.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Degenerate Moslem, a cad!' Eva saved a camel so Meta reneged.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Was it felt? I had a hit left, I saw.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Won't I repaper? Repaper it now.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Too far, Edna, wander afoot.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Stella won no wallets.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Do nine men interpret?' 'Nine men,' I nod.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Nurse, I spy gypsies, run!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Draw pupil's pup's lip upward.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Lewd did I live, and, Edna, evil I did dwel.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Sirrah! Deliver deified desserts detartrated!' stressed deified, reviled Harris.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "All erotic, I lose my lyme solicitor, Ella.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No, is Ivy's order a red rosy vision?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No word, no bond, row on.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "On tub, Edward imitated a cadet; a timid raw debut, no?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Tense, I snap Sharon roses, or Norah's pansies net.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Deliver desserts,' demanded Nemesis, 'emended, named, stressed, reviled.'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No, it is opposed; Art sees Trade's opposition.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Knight, I ask nary rank,' saith gink.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Moors dine, nip -- in Enid's room.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Ma,' Jerome raps pot top, 'spare more jam!'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Live not on evil deed, live not on evil.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Sir, I'm Iris!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Now do I repay a period won.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "A rod, not a bar, a baton, Dora.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Now saw ye no mosses or foam, or aroma of roses. So money was won.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Amiced was I ere I saw Decima.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Pooh,' smiles Eva, 'have Selim's hoop.'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No, set a maple here, help a mate, son.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "A war at Tarawa!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Some men interpret nine memos.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Delia sailed as sad Elias ailed.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ned, I am a maiden.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Dennis sinned.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Remit Rome cargo to go to Grace Mortimer.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Did Dean aid Diana? Ed did.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "I told Edna how to get a mate: 'Go two-handed.' Loti.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Sire, was I ere I saw Eris?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Now Eve, we're here, we've won.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Suit no regrets.' A motto, Master Gerontius.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Eve, man, am Eve!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Slap-dab set-up, Mistress Ann asserts, imputes bad pals.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Tuna nut.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Saladin enrobes a baroness, Senora, base-born Enid, alas.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Deny me not; atone, my Ned.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Roy Ames, I was a wise mayor.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Wonders in Italy, Latin is 'Red' now.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Tis Ivan on a visit.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Did Ione take Kate? No, I did.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Solo gigolos.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No, it is open on one position.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "He Goddam Mad Dog, Eh?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ned, go gag Ogden.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Draw no dray a yard onward.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Revolt, love!' raved Eva. 'Revolt, lover!'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Enid and Edna dine.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Anne, I stay a day at Sienna.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Senile felines.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Max, I stay away at six A.M.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ere hypocrisies or poses are in, my hymn I erase. So prose I, sir, copy here.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "St. Simon sees no mists.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Draw, O coward!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Top step's pup's pet spot.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Noel, did I not rub Burton? I did, Leon.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Puss, a legacy! Rat in a snug, unsanitary cage, lass, up!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Rise to vote, Sir.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Noel saw I was Leon.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Now, sir, a war is won!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ha! Robed Selim smiles, Deborah!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Degas, are we not drawn onward, we freer few, drawn onward to new eras aged?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Now ere we nine were held idle here, we nine were won.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Yo! Bottoms up, U.S. Motto, boy!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Nor I nor Emma had level'd a hammer on iron.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Al lets Della call Ed, Stella.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No Dot nor Ottawa, 'legal age' law at Toronto, Don.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Yes, Mark, cable to hotel, 'Back Ramsey.'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Was it a bar or a bat I saw?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Marge lets Norah see Sharon's telegram.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Analytic Paget saw an inn in a waste-gap city, Lana.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Was raw tap ale not a reviver at one lap at Warsaw?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Live on, Time; emit no evil.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Not for Cecil?' asks Alice Crofton.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ban campus motto, 'Bottoms up, MacNab.'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "So may Apollo pay Amos.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Norma is as selfless as I am, Ron.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Arden saw I was Nedra.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Won't lovers revolt now?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Do not start at rats to nod.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ha! On, on, o Noah!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Norah's foes order red rose of Sharon.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "I made border bard's drowsy swords; drab, red-robed am I.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Emil, asleep, Hannah peels a lime.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Set a broom on no moor, Bates.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ten dip a rapid net.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "O render gnostic illicit song, red Nero.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Are we not drawn onwards, we Jews, drawn onward to new era?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Mother at song no star, eh Tom?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "I maim nine men in Saginaw; wan, I gas nine men in Miami.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "So may get Arts award. Draw a strategy, Amos.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Nella, demand a lad named Allen.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Yes, Syd, Owen saved Eva's new Odyssey.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Now dine,' said I as Enid won.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Lepers repel.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "May a moody baby doom a yam?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Draw -- aye, no melody -- dole-money award.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Mirth, sir, a gay asset? No, don't essay a garish trim.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "See few owe fees.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Stressed was I ere I saw desserts.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Name I -- Major-General Clare -- negro Jamie Man.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Tennis set won now Tess in net.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ewer of miry rim for ewe.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Sh! Tom sees moths.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No misses ordered roses, Simon.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Stop, Syrian, I start at rats in airy spots'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "I, Marian, I too fall; a foot-in-air am I.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Evade me, Dave.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Delia's debonair dahlias, poor, drop, or droop. Sail, Hadrian; Obed sailed.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No ham came, sir, now siege is won. Rise, MacMahon.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Now Ned, I am a maiden nun; Ned, I am a maiden won.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ten animals I slam in a net.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Did I draw Della too tall, Edward? I did?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Go hang a salami, I'm a lasagna hog.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Di, did I as I said I did?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Milestones? Oh, 'twas I saw those, not Selim.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No, it's a bar of gold, a bad log for a bastion.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Naomi, sex at noon taxes', I moan.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Snug & raw was I ere I saw war & guns.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Doc, note, I dissent. A fast never prevents a fatness. I diet on cod.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Live was I ere I saw evil.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Red now on level -- no wonder.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Stop! Murder us not tonsured rumpots!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Step on hose-pipes? Oh no, pets.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Stiff, o dairyman, in a myriad of fits.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "To nets, ah, no, son, haste not.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Dennis, no misfit can act if Simon sinned.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Revered now I live on. O did I do no evil, I wonder ever?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Sore was I ere I saw Eros.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Noel, lets egg Estelle on.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "In a regal age ran I.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Novrad,' sides reversed, is 'Darvon.'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Name now one man.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Dennis and Edna sinned.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Nora, alert, saws goldenrod-adorned logs, wastrel Aaron!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Sums are not set as a test on Erasmus.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Deliver, Eva, him I have reviled.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Damosel, a poem? A carol? Or a cameo pale? (So mad!)", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Doom an evil deed, liven a mood.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "God, a red nugget! A fat egg under a dog!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Nurse's onset abates, noses run.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Roy, am I mayor?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ron, Eton mistress asserts I'm no tenor.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "I tip away a wapiti.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Adelberta was I ere I saw a trebled 'A'.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Sit on a potato pan, Otis.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Moorgate got nine men in to get a groom.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Repel evil as a live leper.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Eva, can I stab bats in a cave?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Did Hannah say as Hannah did?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Goddesses so pay a possessed dog.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Eva, Lave!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ah, Satan sees Natasha.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Do Good's deeds live on? No, Evil's deeds do, O God.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Madame, not one man is selfless; I name not one Madam.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Dora tended net, a rod.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Golf, No, sir, prefer prison flog.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Nella risks all: 'I will ask Sir Allen.'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Now do I report 'Sea Moth' to Maestro, period? Won.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Selim's tired, no wonder, it's miles.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'So I darn on,' a Canon radios.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "War-distended nets I draw.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Stephen, my hat! Ah, what a hymn, eh, pets?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Mad Zeus, no live devil, lived evil on Suez dam.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Bog dirt up a sidetrack carted is a putrid gob.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Mad? Am I, madam?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Madam, in Eden I'm Adam!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ward nurses run 'draw.'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Live dirt up a sidetrack carted is a putrid evil.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Oh who was it I saw, oh who?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Reviled did I live,' said I, 'as evil I did deliver.'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Live was I ere I saw Evil.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Pull up, Eva, we're here, wave, pull up.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Revolt on Yale, Democrats edit 'Noon-Tide Star.' Come, delay not lover.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Won race, so loth to lose car now.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No, it never propagates if I set a 'gap' or prevention.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Delia sailed, Eva waved, Elias ailed.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "I maim nine more hero-men in Miami.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Rise, morning is red, no wonder-sign in Rome, Sir.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Emil asleep, Allen yodelled 'Oy.' Nella peels a lime.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No. I save on final perusal, a sure plan if no evasion.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Yreka Bakery.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "He lived as a devil, eh?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "I saw thee, madame, eh? 'Twas I.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Dior Droid.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Regard a mere mad rager.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "I saw desserts; I'd no lemons, alas no melon. Distressed was I.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "A dog! A panic in a pagoda!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Yawn a more Roman way.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Semite, be sure! Damn a man-made ruse betimes.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Pull up if I pull up.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Able was I ere I saw Elba.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Eve saw diamond, erred, no maid was Eve.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Slang is not suet, is it?' Euston signals.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "I roamed under it as a tired, nude Maori.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Pa's a sap.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No, miss, it is Simon.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Step on no pets!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Niagara, O roar again!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Lewd did I live; evil I did dwel.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Too bad, I hid a boot.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Rats gnash teeth,' sang Star.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Lapp, Mac? No, sir, prison-camp pal.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Tide-net safe, soon, Allin. A manilla noose fastened it.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "I moan, Naomi.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Too far away, no mere clay or royal ceremony, a war afoot.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Rats live on no evil star.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Deer frisk, sir, freed.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "I did roar again, Niagara! ... or did I?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No evil Shahs live on.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "O gnats, tango!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Stop, Syrian, I start at rats in airy spots.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Not New York,' Roy went on.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Norah's moods,' Naomi moans, 'doom Sharon.'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Eva, can I pose as Aesop in a cave?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Trade ye no mere moneyed art.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Was it a rat I saw?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Help Max, Enid -- in example, 'H.'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "So may Obadiah, even in Nineveh, aid a boy, Amos.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "See, slave, I demonstrate yet arts no medieval sees.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Egad, a base tone denotes a bad age.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Lew, Otto has a hot towel.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Warden in a Cap,' Mac's pup scamp, a canine draw.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Paget saw an inn in a waste gap.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "A slut nixes sex in Tulsa.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Must sell at tallest sum.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Naomi, did I moan?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Drab Red, no londer bard.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Revenge my baby, meg? Never!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Stop, Syrian, I see bees in airy spots.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Did I do, O God, did I as I said I'd do? Good, I did!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Pusillanimity obsesses Boy Tim in 'All Is Up.'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Refasten Gipsy's pig-net safer.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Pat and Edna tap.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Adam, I'm Ada!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ma is as selfless as I am.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Poor Dan is in a droop.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Leon sees Noel.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "So may Obadiah aid a boy, Amos.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Sis, Sargasso moss a grass is.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Now, Ned, I am a maiden won.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "I moan, 'Live on, O evil Naomi!'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Delia and Edna ailed.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "So remain a mere man. I am Eros.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No lemons, no melon.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Never odd or even.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Rise, sir lapdog! Revolt, lover! God, pal, rise, sir!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ah, Aristides opposed it, sir, aha!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ma is a nun, as I am.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Madam, I'm Adam.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Star? Come, Donna Melba, I'm an amiable man -- no Democrats!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "We'll let Dad tell Lew.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No, it is opposition.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No benison, no sin, Ebon.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ladle histolytic city lots I held, Al.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Harass selfless Sarah!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ten? No bass orchestra tarts, eh? Cross a bonnet!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Si, we'll let Dad tell Lewis.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "In airy Sahara's level, Sarah, a Syrian, I.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Nor I, fool, ah no? We won halo -- of iron.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "We seven, Eve, sew.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Peel's lager on red rum did murder no regal sleep.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Go, droop aloof,' sides reversed, is 'fool a poor dog.'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Sir, I soon saw Bob was no Osiris.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "St. Eloi, venin saved a mad Eva's nine violets.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Here so long? No loser, eh?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Flee to me, remote elf.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Six at party, no pony-trap, taxis.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Drab as a fool, as aloof as a bard.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Red? Rum, eh? 'Twas I saw the murder.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Pull a bat! I held a ladle, hit a ball up.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "No, Hal, I led Delilah on.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Nomists reign at Tangier, St. Simon.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Nemo, we revere women.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Harass sensuousness, Sarah.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Are we not, Rae, near to new era?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Red root put up to order.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "'Sal is not in?' Ruth asks. 'Ah, turn it on, Silas.'", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Retracting, I sign it, Carter.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "A Toyota.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "He won a Toyota now, eh?", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Gate-man sees name, garage-man sees name-tag.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Live not on evil.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Nella won't set a test now, Allen.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Ha! I rush to my lion oily moths, Uriah!", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Live dirt, up a side-track carted, is a putrid evil.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Dog, as a devil deified, lived as a god.", ""));
            Puzzles.Add(new PalindromePuzzle(sender.Device, "Not so, Boston.", ""));
        }

        private void canvasMain_PointerMoved(object sender, PointerRoutedEventArgs e) {

        }

        private void canvasMain_PointerPressed(object sender, PointerRoutedEventArgs e) {

        }

        private void canvasMain_PointerReleased(object sender, PointerRoutedEventArgs e) {

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e) {
            // await RecordSpeechFromMicrophoneAsync();
            // int i = 0;
        }
    }
}
