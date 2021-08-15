// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to Festival Manager (entities/controllers/etc)
// Test ONLY the Stage class. 

using System.Collections.Generic;
using System.Linq;
using FestivalManager.Entities;

namespace FestivalManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
	public class StageTests
    {

        private Song song;
        private Stage stage;
        private Performer performers;

        [SetUp]
        public void Setup()
        {
            song = new Song("pesen", new TimeSpan(1,1,1));
            stage = new Stage();
            performers = new Performer("perfName", "perflastName", 29);
        }

        [Test]
        public void PerformerCannotBeNullInAddPerformer()
        {
            Assert.Throws<ArgumentNullException>
                (() => stage.AddPerformer(null));
        }

        [Test]
        public void WhenAddPerformerAgeLessThan18ThrowsError()
        {
            Assert.Throws<ArgumentException>
                (() => stage.AddPerformer(new Performer("pesh","peshov",17)));

        }


        [Test]
        public void WhenAddPerformerAllSuccessful()
        {
            stage.AddPerformer(performers);

            Assert.AreEqual(1, stage.Performers.Count);

        }

        [Test]
        public void SongCannotBeNullInAddSong()
        {
            Assert.Throws<ArgumentNullException>
                (() => stage.AddSong(null));
        }

        [Test]
        public void WhenAddSongDurationLessThanMinThrowsError()
        {
            Assert.Throws<ArgumentException>
                (() => stage.AddSong(new Song("pesen",new TimeSpan(0,0,30))));

        }

        [Test]
        public void WhenAddSongAllSuccessful()
        {
            stage.AddSong(song);

            Assert.AreEqual("pesen", song.Name);

        }

        [Test]
        public void WhenAddSongToPerformerSongNameCannotBeNull()
        {
            Assert.Throws<ArgumentNullException>
                (() => stage.AddSongToPerformer(null, "neshto"));
        }

        [Test]
        public void WhenAddSongToPerformerPerformerNameCannotBeNull()
        {
            Assert.Throws<ArgumentNullException>
                (() => stage.AddSongToPerformer("neshto", null));
        }

        [Test]
        public void WhenAddSongToPerformerGetPerformer()
        {
            var performer = new Performer("pesho", "petrov", 23);
            var song = new Song("pesen", new TimeSpan(0, 1, 34));

            stage.AddPerformer(performer);
            stage.AddSong(song);

            stage.AddSongToPerformer("pesen", "pesho petrov");
            
            Assert.That(performer.SongList.Count==1);
            Assert.That(performer.SongList.FirstOrDefault().Equals(song));
        }

        [Test]
        public void StagePlayReturnsRightNumbers()
        {
            var performer = new Performer("pesho", "petrov", 23);
            var song = new Song("pesen", new TimeSpan(0, 1, 34));
            
            stage.AddPerformer(performer);
            stage.AddSong(song);

            stage.AddSongToPerformer("pesen", "pesho petrov");
            string result = stage.Play();

            Assert.That(result== "1 performers played 1 songs");

        }

        [Test]
        public void AddSongToPerformerSongNotExist()
        {
            var performer = new Performer("pesho", "petrov", 23);
            
            stage.AddPerformer(performer);
            

            Assert.Throws<ArgumentException>
                (() => stage.AddSongToPerformer("pesen", "pesho petrov"));
        }

        [Test]
        public void AddSongToPerformerPerformerDoNotExist()
        {
            var song = new Song("pesen", new TimeSpan(0, 1, 34));

            
            stage.AddSong(song);

            Assert.Throws<ArgumentException>
                (() => stage.AddSongToPerformer("pesen", "pesho petrov"));
        }

        
    }
}