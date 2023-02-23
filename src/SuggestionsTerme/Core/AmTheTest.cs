using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuggestionsTerme.Core
{
    public class AmTheTest : IAmTheTest
    {
        /// <summary>
        /// Retourne une liste du nombre spécifié de termes alphanumériques en minuscules à partir
        /// d'une liste de choix qui contiennent le terme spécifié ou qui en sont les plus similaires.
        /// La similarité est déterminée par le nombre de lettres à remplacer pour que le terme corresponde,
        /// sans insérer de lettres. Si plusieurs choix ont le même score de différence, les termes les plus proches
        /// en longueur et triés par ordre alphabétique sont préférés.
        /// </summary>
        /// <param name="term">Le terme.</param>
        /// <param name="choices">La liste </param>
        /// <param name="numberOfSuggestions">Le nombre maximal de suggestions à retourner.</param>
        /// <returns>Un énumérable ordonné des suggestions sélectionnées avec les suggestions demandés.</returns>
        public IEnumerable<string> GetSuggestions(string term, IEnumerable<string> choices, int numberOfSuggestions)
        {
            if (term is null || choices.Any(x => string.IsNullOrEmpty(x)))
                throw new ArgumentException("Valeurs invalides !");

            var suggestions = new Dictionary<string, int>();

            //trier la liste des choices avec longeur et ordre alphanumériques
            var sortedChoices = choices.OrderBy(c => c.Length).ThenBy(c => c).ToList();
            //parcouric la liste triés pour sorter chaque choice avec le nombre de différence
            foreach (var choice in sortedChoices)
            {
                //si la longeur < terme on continue la boucle 
                if (choice.Length < term.Length)
                    continue;
                //sinon on calcule le reste 
                int differenceScore = GetDifferenceScore(choice.Substring(0, term.Length), term);
                //ajouter dans le dictionnaire chaque avec le score de differnce
                if (!(differenceScore >= term.Length))//si longeur>= longeur terme c'est unitile de le rajouter la suggestions
                    suggestions.Add(choice, differenceScore);

                ///DISCLAIMER : il y a une autre méthode sans inserer dans le dictionnaire en utilisant des calcul sur la longeur,mais
                ///pour des raisons de debug j'ai implementé ce code afin d'avoir plus de détails sur le score de chaque choice.

            }


            // les termes les plus proches en longueur
            var firstSuggestions = suggestions.OrderBy(choice => choice.Value)
                                               .Select(pair => pair.Key)
                                               .Take(numberOfSuggestions);

            return firstSuggestions;
        }
        /// <summary>
        /// Cette méthode prend deux chaînes de caractères en entrée et retourne le score de différence entre elles.
        /// Le score de différence est déterminé en comparant les caractères des deux chaînes à chaque position et en comptant le nombre de 
        /// fois où ils diffèrent. La méthode parcourt chaque caractère de la chaîne de destination et vérifie s'il est présent dans la chaîne source.
        /// Si ce n'est pas le cas, cela signifie que les deux caractères diffèrent et le compteur est incrémenté. 
        /// Le résultat final est le nombre de caractères différents entre les deux chaînes.
        /// </summary>
        /// <param name="dest">chaine destination</param>
        /// <param name="src">chaine source</param>
        /// <returns>nombre de caractères différents entre les deux chaînes</returns>
        private int GetDifferenceScore(string dest, string src)
        {

            int count = 0;
            for (int i = 0; i < dest.Length; i++)
            {
                if (!src.Contains(dest[i]))
                {
                    count++;
                }
            }
            return count;

        }
    }
}
