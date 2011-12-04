using System;
using System.Linq;
using LinqToTwitter;
using TweetSharp.Twitter.Model;

namespace NetMag.RedesSociais.Exemplos.Twitter
{
    public static class ExemplosLinqToTwitter
    {
        public static void ObterTweetsPublicos(TwitterContext twitterCtx)
        {
            var tweetsPublicos =
                from tweet in twitterCtx.Status
                where tweet.Type == StatusType.Public
                select tweet;

            tweetsPublicos.ToList().ForEach(
                tweet => Console.WriteLine(
                    "\n\nUsuário: {0}, \nTweet: {1}",
                    tweet.User.Name,
                    tweet.Text));
        }

        public static void ObterTweetsPublicosHome(TwitterContext twitterCtx)
        {
            var tweetsDeAmigos =
                from tweet in twitterCtx.Status
                where tweet.Type == StatusType.Home &&
                      tweet.Page == 2
                select tweet;

            Console.WriteLine("Tweets para " + twitterCtx.UserName + "\n");
            foreach (var tweet in tweetsDeAmigos)
            {
                Console.WriteLine(
                    "Amigo: " + tweet.User.Name +
                    "\nRetweetado por: " +
                        (tweet.Retweet == null ?
                            "Tweet original" :
                            tweet.Retweet.RetweetingUser.Name) +
                    "\nTweet {0}: " + tweet.Text + "\n", tweet.ID);
            }
        }

        public static void ObterMeusTweets(TwitterContext twitterCtx)
        {
            var meusTweets =
                from tweet in twitterCtx.Status
                where tweet.Type == StatusType.User
                      && tweet.UserID == "148784321"  
                select tweet;

            foreach (var tweet in meusTweets)
            {
                Console.WriteLine(
                    "(" + tweet.StatusID + ")" +
                    "[" + tweet.User.ID + "]" +
                    tweet.User.Name + ", " +
                    tweet.Text + ", " +
                    tweet.CreatedAt);
            }
        }

        public static void ObterMinhasInformacoes(TwitterContext twitterCtx)
        {
            var usuarios =
                from tweet in twitterCtx.User
                where tweet.Type == UserType.Show &&
                      tweet.ScreenName == "dantovcorp"
                select tweet;

            var usuario = usuarios.SingleOrDefault();
            var nome = usuario.Name;
            var id = usuario.ID;
            var ultimoStatus = usuario.Status == null ? "sem status" : usuario.Status.Text;

            Console.WriteLine();
            Console.WriteLine("Nome: {0} [{1}] \nÚltimo tweet: {2}\n", nome, id, ultimoStatus);
        }

        public static void ObterListaSeguidores(TwitterContext twitterCtx)
        {
            var users =
                from tweet in twitterCtx.User
                where tweet.Type == UserType.Followers &&
                      tweet.ScreenName == "dantovcorp"
                select tweet;

            foreach (var user in users)
            {
                var status =
                    user.Protected || user.Status == null
                        ? "Status indisponível"
                        : user.Status.Text;

                Console.WriteLine(
                    "Nome: {0} \nÚltimo tweet: {1}\n",
                    user.Name, status);
            }
        }

        public static void ObterListaAmigos(TwitterContext twitterCtx)
        {
            var usuarios =
                from tweet in twitterCtx.User
                where tweet.Type == UserType.Friends &&
                      tweet.ScreenName == "dantovcorp"
                select tweet;

            foreach (User usuario in usuarios)
            {
                var status =
                    usuario.Protected || usuario.Status == null
                        ? "Status indisponível"
                        : usuario.Status.Text;

                Console.WriteLine(
                    "Nome: {0} \nÚltimo tweet: {1}\n",
                    usuario.Name, status);
            }
        }

        public static void CriarNovoTweet(TwitterContext twitterCtx)
        {
            var status = "Tweetando através do #linqtotwitter! " + DateTime.Now;
            var tweet = twitterCtx.UpdateStatus(status);
            Console.WriteLine("Tweet atualizado (id: {0})!", tweet.ID);
        }

        public static void ObterMencoes(TwitterContext twitterCtx)
        {
            var minhasMencoes =
                from mention in twitterCtx.Status
                where mention.Type == StatusType.Mentions
                select mention;

            minhasMencoes.ToList().ForEach(
                mention => Console.WriteLine(
                    "Nome: {0}, Tweet[{1}]: {2}\n",
                    mention.User.Name, mention.StatusID, mention.Text));
        }

        public static void EnviandoUmaMensagemDireta(TwitterContext twitterCtx, string ID)
        {
            var message = twitterCtx.NewDirectMessage(ID, 
                "Enviando uma mensagem direta de teste em " + DateTime.Now);
            Console.Write("Mensagem enviada, com o id: {0}!", message.ID);
        }

        public static void Retweetando(TwitterContext twitterCtx, string tweetID)
        {
            var retweet = twitterCtx.Retweet(tweetID);
            Console.Write("Retweet feito, a partir do usuário com o id: {0}!", 
                retweet.InReplyToUserID);
        }

        public static void VerificarRelacionamento(TwitterContext twitterCtx, string amigo, string segue)
        {
            var relacionamento =
                (from friend in twitterCtx.Friendship
                 where friend.Type == FriendshipType.Exists &&
                       friend.SubjectUser == amigo &&
                       friend.FollowingUser == segue
                 select friend)
                    .ToList();

            Console.WriteLine("{0} segue {1}? " +
                relacionamento.First().IsFriend, amigo, segue);
        }
    }
}