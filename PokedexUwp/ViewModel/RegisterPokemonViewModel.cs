using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Connection.Dispatchers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;

namespace PokedexUwp.ViewModel
{
    public class RegisterPokemonViewModel : ObservableObject
    {
        #region Variables

        private Dictionary<int, string> types = new Dictionary<int, string>();

        private string _name;
        private string _typePrimary;
        private string _typeSecundary;
        private string _abilities;
        private string _id;
        private string _xpBase;
        private string _weight;
        private string _height;
        private string _hp;
        private string _attack;
        private string _defense;
        private string _specialAttack;
        private string _specialDefense;
        private string _speed;
        private BitmapImage _image;
        private string _outPutTextImage;
        private StorageFile _file;
        private string _pathImage;

        #endregion

        #region Properties

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string TypePrimary
        {
            get => _typePrimary;
            set => SetProperty(ref _typePrimary, value);
        }
        public string TypeSecundary
        {
            get => _typeSecundary;
            set => SetProperty(ref _typeSecundary, value);
        }
        public string Abilities
        {
            get => _abilities;
            set => SetProperty(ref _abilities, value);
        }
        public string XpBase
        {
            get => _xpBase;
            set => SetProperty(ref _xpBase, value);
        }
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        public string Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }
        public string Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }
        public string Hp
        {
            get => _hp;
            set => SetProperty(ref _hp, value);
        }
        public string Attack
        {
            get => _attack;
            set => SetProperty(ref _attack, value);
        }
        public string Defense
        {
            get => _defense;
            set => SetProperty(ref _defense, value);
        }
        public string SpecialAttack
        {
            get => _specialAttack;
            set => SetProperty(ref _specialAttack, value);
        }
        public string SpecialDefense
        {
            get => _specialDefense;
            set => SetProperty(ref _specialDefense, value);
        }
        public string Speed
        {
            get => _speed;
            set => SetProperty(ref _speed, value);
        }
        public BitmapImage Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }
        public StorageFile File
        {
            get => _file;
            set => SetProperty(ref _file, value);
        }
        public string OutPutTextImage
        {
            get => _outPutTextImage;
            set => SetProperty(ref _outPutTextImage, value);
        }
        public string PathImage
        {
            get => _pathImage;
            set => SetProperty(ref _pathImage, value);
        }

        #endregion

        #region ICommands

        public ICommand LoadImage { get; }
        public ICommand SavePokemon { get; }
        public ICommand CancelPokemon { get; }

        #endregion

        public RegisterPokemonViewModel()
        {
            InitializeViewModel();

            SavePokemon = new RelayCommand(SavedPokemon);
            LoadImage = new RelayCommand(LoadImageView);
            CancelPokemon = new RelayCommand(CancelPokemonView);
        }

        #region Private Methods

        #region Commands Methods

        private void InitializeViewModel()
        {
            int initialId = App.BoPokemonDataBase.GetMaxPokemonIndex() + 1;
            Id = initialId > 260 ? initialId.ToString() : "260";
        }

        private async void LoadImageView()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var picker = new FileOpenPicker();
                picker.ViewMode = PickerViewMode.Thumbnail;
                picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                picker.FileTypeFilter.Add(".jpg");
                picker.FileTypeFilter.Add(".jpeg");
                picker.FileTypeFilter.Add(".png");

                File = await picker.PickSingleFileAsync();
                OutPutTextImage = File.Name;
                if (File != null)
                {
                    using (IRandomAccessStream fileStream = await File.OpenAsync(FileAccessMode.Read))
                    {
                        try
                        {
                            await ApplicationData.Current.LocalFolder.CreateFolderAsync("images", CreationCollisionOption.OpenIfExists);
                            StorageFolder pathPictures = await ApplicationData.Current.LocalFolder.GetFolderAsync("images");
                            await File.CopyAsync(pathPictures, string.Format("{0}.jpg", Id), NameCollisionOption.ReplaceExisting);

                            PathImage = Path.Combine(ApplicationData.Current.LocalFolder.Path, "images");

                            BitmapImage bitmapImage = new BitmapImage();
                            await bitmapImage.SetSourceAsync(fileStream);
                            Image = bitmapImage;
                        }
                        catch (Exception e)
                        {
                            MessageDialog(e.ToString());
                        }

                    }

                }
                else
                {
                    OutPutTextImage = "Falha.";
                }
            }
            else
            {
                MessageDialog("Opção inválido! Por favor digite o ID para poder carregar a image!");
            }
        }

        private async void SavedPokemon()
        {
            try
            {
                Pokemon pokemon = new Pokemon();
                InsertName(pokemon);
                InsertId(pokemon);
                InsertHeight(pokemon);
                InsertWeight(pokemon);
                InsertXpBase(pokemon);

                pokemon.Types = new List<TypeElement>();
                pokemon.Stats = new List<StatElement>();

                InsertTypePrimary(pokemon);
                if (!TypeSecundary.Equals("none"))
                    InsertTypeSecundary(pokemon);

                InsertHp(pokemon);
                InsertAttack(pokemon);
                InsertDefense(pokemon);
                InsertSpecialAttack(pokemon);
                InsertSpecialDefense(pokemon);
                InsertSpeed(pokemon);

                InsertSprites(pokemon);
                InsertAbilities(pokemon);

                await App.BoPokemonDataBase.AddPokemon(pokemon);

                MessageDialog("Pokemon Salvo!!");

                Image = null;
                OutPutTextImage = string.Empty;
            }
            catch (Exception ex)
            {
                MessageDialog(ex.Message);
            }
        }

        private void CancelPokemonView()
        {
            Id = string.Empty;
            Name = string.Empty;
            TypePrimary = string.Empty;
            TypeSecundary = string.Empty;
            Abilities = string.Empty;
            XpBase = string.Empty;
            Weight = string.Empty;
            Height = string.Empty;
            Hp = string.Empty;
            Attack = string.Empty;
            Defense = string.Empty;
            SpecialAttack = string.Empty;
            SpecialDefense = string.Empty;
            Speed = string.Empty;
            Image = null;
            OutPutTextImage = string.Empty;
            PathImage = string.Empty;
        }

        #endregion

        private static void InsertAbilities(Pokemon pokemon)
        {
            AbilityElement abilityElement = new AbilityElement();
            TypeClass typeClass = new TypeClass();

            typeClass.Name = string.Empty;
            abilityElement.Ability = typeClass;
            List<AbilityElement> abilities = new List<AbilityElement>();
            abilities.Add(abilityElement);
            pokemon.Abilities = abilities;
        }

        private void InsertSprites(Pokemon pokemon)
        {
            Sprites sprites = new Sprites();
            Other other = new Other();
            Home home = new Home();
            home.Front_Default = PathImage + "\\" + pokemon.Id + ".jpg";
            other.Home = home;
            sprites.Other = other;
            pokemon.Sprites = sprites;
        }

        private void InsertSpeed(Pokemon pokemon)
        {
            StatStat stat = new StatStat();
            StatElement listStat = new StatElement();
            if (Speed.All(char.IsDigit))
            {
                stat.Name = "speed";
                var auxSpeed = short.Parse(Speed);
                listStat.Base_Stat = auxSpeed;
                listStat.Stat = stat;
                pokemon.Stats.Add(listStat);
                Speed = string.Empty;
            }
            else
            {
                stat.Name = "speed";
                listStat.Stat = stat;
                pokemon.Stats.Add(listStat);
                Speed = string.Empty;
            }
        }

        private void InsertSpecialDefense(Pokemon pokemon)
        {
            StatStat stat = new StatStat();
            StatElement listStat = new StatElement();
            if (SpecialDefense.All(char.IsDigit))
            {
                stat.Name = "special-defense";
                var auxSDefense = short.Parse(SpecialDefense);
                listStat.Base_Stat = auxSDefense;
                listStat.Stat = stat;
                pokemon.Stats.Add(listStat);
                SpecialDefense = string.Empty;
            }
            else
            {
                stat.Name = "special-defense";
                listStat.Stat = stat;
                pokemon.Stats.Add(listStat);
                SpecialDefense = string.Empty;
            }
        }

        private void InsertSpecialAttack(Pokemon pokemon)
        {
            StatStat stat = new StatStat();
            StatElement listStat = new StatElement();
            if (SpecialAttack.All(char.IsDigit))
            {
                stat.Name = "special-attack";
                var auxSAttack = short.Parse(SpecialAttack);
                listStat.Base_Stat = auxSAttack;
                listStat.Stat = stat;
                pokemon.Stats.Add(listStat);
                SpecialAttack = string.Empty;
            }
            else
            {
                stat.Name = "special-attack";
                listStat.Stat = stat;
                pokemon.Stats.Add(listStat);
                SpecialAttack = string.Empty;
            }
        }

        private void InsertDefense(Pokemon pokemon)
        {
            StatStat stat = new StatStat();
            StatElement listStat = new StatElement();
            if (Defense.All(char.IsDigit))
            {
                stat.Name = "defense";
                var auxDefense = short.Parse(Defense);
                listStat.Base_Stat = auxDefense;
                listStat.Stat = stat;
                pokemon.Stats.Add(listStat);
                Defense = string.Empty;
            }
            else
            {
                stat.Name = "defense";
                listStat.Stat = stat;
                pokemon.Stats.Add(listStat);
                Defense = string.Empty;
            }
        }

        private void InsertAttack(Pokemon pokemon)
        {
            StatStat stat = new StatStat();
            StatElement listStat = new StatElement();
            if (Attack.All(char.IsDigit))
            {
                stat.Name = "attack";
                var auxAttack = short.Parse(Attack);
                listStat.Base_Stat = auxAttack;
                listStat.Stat = stat;
                pokemon.Stats.Add(listStat);
                Attack = string.Empty;
            }
            else
            {
                stat.Name = "attack";
                listStat.Stat = stat;
                pokemon.Stats.Add(listStat);
                Attack = string.Empty;
            }
        }

        private void InsertHp(Pokemon pokemon)
        {
            StatStat stat = new StatStat();
            StatElement listStat = new StatElement();
            if (Hp.All(char.IsDigit))
            {
                stat.Name = "hp";
                var auxHp = short.Parse(Hp);
                listStat.Base_Stat = auxHp;
                listStat.Stat = stat;
                pokemon.Stats.Add(listStat);
                Hp = string.Empty;
            }
            else
            {
                stat.Name = "hp";
                listStat.Stat = stat;
                pokemon.Stats.Add(listStat);
                Hp = string.Empty;
            }
        }

        private void InsertTypeSecundary(Pokemon pokemon)
        {
            TypeClass type = new TypeClass();
            type.Name = TypeSecundary;
            TypeElement listType = new TypeElement();
            listType.Type = type;
            pokemon.Types.Add(listType);
            TypeSecundary = string.Empty;
        }

        private void InsertTypePrimary(Pokemon pokemon)
        {
            TypeClass type = new TypeClass();
            type.Name = TypePrimary;
            TypeElement listType = new TypeElement();
            listType.Type = type;
            pokemon.Types.Add(listType);
            TypePrimary = string.Empty;
        }

        private void InsertXpBase(Pokemon pokemon)
        {
            if (XpBase.All(char.IsDigit) && !string.IsNullOrEmpty(XpBase))
            {
                var auxXpBase = short.Parse(XpBase);
                pokemon.Base_Experience = auxXpBase;
                XpBase = string.Empty;
            }
            XpBase = string.Empty;
        }

        private void InsertWeight(Pokemon pokemon)
        {
            if (Weight.All(char.IsDigit) && !string.IsNullOrEmpty(Weight))
            {
                var auxPeso = short.Parse(Weight);
                pokemon.Weight = auxPeso;
                Weight = string.Empty;
            }
            Weight = string.Empty;
        }

        private void InsertHeight(Pokemon pokemon)
        {
            if (Height.All(char.IsDigit))
            {
                var auxAltura = short.Parse(Height);
                pokemon.Height = auxAltura;
                Height = string.Empty;
            }
            Height = string.Empty;
        }

        private void InsertId(Pokemon pokemon)
        {
            if (int.TryParse(Id, out int auxId))
            {
                pokemon.Id = auxId;

                if (App.BoPokemonDataBase.HasPokemonById(Id))
                {
                    Id = string.Empty;
                    throw new Exception("Já existe um Pokemon com esse Id!");
                }
                Id = string.Empty;
            }
            else
            {
                Id = string.Empty;
                throw new Exception("Id do Pokemon inválido! Por favor digite apenas Numero!");
            }
        }

        private void InsertName(Pokemon pokemon)
        {
            pokemon.Name = Name;
            Name = string.Empty;

            if (!pokemon.Name.All(char.IsLetter))
                throw new Exception("Nome do Pokemon inválido! Por favor digite apenas Letras!");
        }

        private async void MessageDialog(string msg)
        {
            var dialog = new MessageDialog(msg);
            await dialog.ShowAsync();
        }

        #endregion
    }
}
