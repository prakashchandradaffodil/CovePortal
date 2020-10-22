using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Cove.Web.Models
{
    public class UploadComicViewModel
    {
        //[Required(ErrorMessage ="Please Upload Comic in PDF format")]
        //Upload file Format should be PDF Maximum file size is 500MB  The creator will see a message on the screen to indicate minimum DPI "Minimum resolution should be 300 dpi"
        [Display(Name = "Upload Files(PDF)")]
        public List<IFormFile> Files { get; set; }
        public object UploadedFiles { get; set; }

        [Required]
        [Display(Name = "Creator(s)")]
        public string Creator { get; set; }

        [Display(Name = "Series Title")]
        public string SeriesTitle { get; set; }

        [Display(Name = "Issue title")]
        public string IssueTitle { get; set; }

      
        [Required(ErrorMessage = "Price is required")]
        [RegularExpression(@"^\d+\.\d{0,4}$",ErrorMessage = "Enter Price (4 digit after decimal allowed)")]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Issue Number")]
        [StringLength(4, ErrorMessage = "Issue Number can not be more than 4 digit")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid Issue Number (4 digit numeric allowed)")]

        public string IssueNumber { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(1000, ErrorMessage = "Description  length shoud be between 10-1000 characters.", MinimumLength =10)]
        public string Description { get; set; }


        public bool isPublishMyComic { get; set; }

        //RadioButtonList Prepare and Populate and save one selected Value 
        [Required(ErrorMessage = "Age Suitability is Required")]  
        [Display(Name = "Age Suitability")]
        public string selectedAgeSuitability{ get; set; }
        public IEnumerable<SelectListItem> AgeSuitabilityListRBL { get; set; }
     

        //Gener has two dropdown list
        [Display(Name = "Fiction")]
        public List<string> SelectedGenerFiction { get; set; }
        public IEnumerable<SelectListItem> FictionListDDL { get; set; }


        [Display(Name = "Non-Fiction")]
        public List<string> SelectedGenerNonFiction { get; set; }
        public IEnumerable<SelectListItem> NonFictionListDDL { get; set; }

        //tag has two dropdown list
        [Display(Name = "Content Warnings")]
        public List<string> SelectedTagContentWarning { get; set; }
        public IEnumerable<SelectListItem> TagContentWarningListDDL { get; set; }

        [Required(ErrorMessage = "Tag Type Selection is Required")]
        [Display(Name = "Types")]
        public List<string> SelectedTagTypes { get; set; }
        public IEnumerable<SelectListItem> TagTypesListDDL { get; set; }

        //[Required(ErrorMessage = "Please Upload thumbnail image")]
        public string ThumbnailImageUrl { get; set; }

        public string Role { get; set; }

        /* Creator(s)
         * 2-100 char   
         * Dynamic text allows selection from creator users with an account(to discuss)
         * creator has an account, and has been selected from a list of account holding creators 
         * the name should become a link to their profile and previous projects
      
        
         * Series Title  
         *  2-100 char 
         * Special characters are allowed 
         * Dynamic text allows selection from previous works (no such work need to discuss)
          
         
         * Issue Title
         *  2-100 char 
         * Special characters are allowed 
         
       
         * Issue Number
         * nUMERIIC value 1-4
         
         
         * Description
         * 10-1000 chars
         
         
         *Genre has two dropdown values fiction and Non-fiction 
         *Drop down values are multi select, but a user can not select from both Fiction and Non-Fiction
         *By default, all values are blank
         *fiction has 25 multiselect option-
            Action/Adventure
          	Chick Lit
          	Classics
          	Crime
          	Detective
          	Drama
          	Fairytale
          	Fantasy
          	Historical Fiction
          	Horror
          	Humour
          	LGBTQ
          	Mystery
          	Paranormalac
          	Political
          	Romance
          	Satire
          	Science Fiction
          	Spy
          	Superhero
          	Suspense
          	Thriller
          	Western
          	Women’s Fiction
          	War
         *Non-fiction has 18 multiselect option
            Art
            Autobiography
            Biography
            Health/Fitness
            History
            Home & Garden
            LGBTQ
            Memoir
            Philosophy
            Poetry
            Religion/Spirituality
            Review
            Science
            Self Help
            Sports & Leisure
            Travel
            True Crime
            War
        

         *Age Suitability” field validation –
         *Radio buttons has 3 values  	1)All Ages  2)Teen and Up  3)Adult
         *Only one radio button can be selected, Clicking on a different button will change the selection,default no selection 


          *Tags has two dropdown “Content Warnings” and  “Type” 
          *Drop down values are multi select,By default, no value should be selected
          *Content Warning is optional but Type is required
                  Blasphemy
                  Character Death
                  Explicit
                  Gore
                  Out Dated Ideals
                  Racism
                  Rape/Non-consent
                  Self Harm
                  Sexism
                  Swearing
                  Violence
          *Type is require
                  Stand alone
                  Mini Series
                  Ongoing Story
                  Anthology
                  Graphic Novel
                  Collection
                  Manga
        
         *Thumbnail(to be discussed) till 14 points in trell0 then contradictory so doing till now
         *the cover (first) page is chosen as the thumbnail by default 
         *the creator can choose to change the thumbnail to an image of their choice from the comic
         *the creator can choose to upload a different image.(format – JPG, maximum size 2mb)
         
        ************TBD last checkbox point  isPublishMyCpmic
        * I would like a representative to contact me to discuss publishing my comic on Get My Comics
        * this will be checkbox for tick if need to discuss for publishing  The text "Get My Comics" will be a link to http://getmycomics.com
        * Selecting this option will send a notification to the Admin team including contact details for the creator:
        * Name	email address 	Telephone number Title of the relevant comic
         
        
        Note : UPLOAD button clicked then Please enter all required fields” if not given
         Given that I have entered a "Series Title", then I must also enter an "Issue Title" and these fields are optional
         Means if we didnot given both are fine ,given issue title but not series is fine but if series given then issue are required 
        */


        public  UploadComicViewModel()
        {
            //isPublishMyComic = false;
            //AgeSuitabilityListRBL = new List<SelectListItem>
            //    {   new SelectListItem() { Text ="All Ages", Value="All Ages"},
            //        new SelectListItem() { Text ="Teen and Up", Value="Teen and Up"},
            //        new SelectListItem() { Text ="Adult", Value="Adult"}
            //    };
            //FictionListDDL = new List<SelectListItem> {
            //       new SelectListItem() { Text ="Action/Adventure"   , Value="Action/Adventure  "},
            //       new SelectListItem() { Text ="Chick Lit"   , Value="Chick Lit         "},
            //       new SelectListItem() { Text ="Classics "   , Value="Classics          "},
            //       new SelectListItem() { Text ="Crime"       , Value="Crime             "},
            //       new SelectListItem() { Text ="Detective"   , Value="Detective         "},
            //       new SelectListItem() { Text ="Drama"       , Value="Drama             "},
            //       new SelectListItem() { Text ="Fairytale"   , Value="Fairytale         "},
            //       new SelectListItem() { Text ="Fantasy"     , Value="Fantasy           "},
            //       new SelectListItem() { Text ="Historical Fiction", Value="Historical Fiction"},
            //       new SelectListItem() { Text ="Horror"      , Value="Horror            "},
            //       new SelectListItem() { Text ="Humour"      , Value="Humour            "},
            //       new SelectListItem() { Text ="LGBTQ "      , Value="LGBTQ             "},
            //       new SelectListItem() { Text ="Mystery"     , Value="Mystery           "},
            //       new SelectListItem() { Text ="Paranormalac", Value="Paranormalac      "},
            //       new SelectListItem() { Text ="Political"   , Value="Political         "},
            //       new SelectListItem() { Text ="Romance"     , Value="Romance           "},
            //       new SelectListItem() { Text ="Satire"      , Value="Satire            "},
            //       new SelectListItem() { Text ="Science Fiction", Value="Science Fiction   "},
            //       new SelectListItem() { Text ="Spy"         , Value="Spy"},
            //       new SelectListItem() { Text ="Superhero"   , Value="Superhero         "},
            //       new SelectListItem() { Text ="Suspense"    , Value="Suspense          "},
            //       new SelectListItem() { Text ="Thriller"    , Value="Thriller          "},
            //       new SelectListItem() { Text ="Western"     , Value="Western           "},
            //       new SelectListItem() { Text ="Women’s Fiction", Value="Women’s Fiction   "},
            //       new SelectListItem() { Text ="War", Value="War"}
            //    };
            //    NonFictionListDDL = new List<SelectListItem> {
            //        new SelectListItem() { Text = "Art"                   , Value="Art"},
            //        new SelectListItem() { Text = "Autobiography"         , Value="Autobiography"},
            //        new SelectListItem() { Text = "Biography"             , Value="Biography"},
            //        new SelectListItem() { Text = "Health/Fitness"        , Value="Health/Fitness"},
            //        new SelectListItem() { Text = "History"               , Value="History"},
            //        new SelectListItem() { Text = "Home & Garden"         , Value="Home & Garden "},
            //        new SelectListItem() { Text = "LGBTQ"                 , Value="LGBTQ"},
            //        new SelectListItem() { Text = "Memoir"                , Value="Memoir"},
            //        new SelectListItem() { Text = "Philosophy"            , Value="Philosophy"},
            //        new SelectListItem() { Text = "Poetry"                , Value="Poetry"},
            //        new SelectListItem() { Text = "Religion/Spirituality" , Value="Religion/Spirituality"},
            //        new SelectListItem() { Text = "Review"                , Value="Review"},
            //        new SelectListItem() { Text = "Science"               , Value="Science"},
            //        new SelectListItem() { Text = "Self Help"             , Value="Self Help"},
            //        new SelectListItem() { Text = "Sports & Leisure"      , Value="Sports & Leisure"},
            //        new SelectListItem() { Text = "Travel"                , Value="Travel"},
            //        new SelectListItem() { Text = "True Crime"            , Value="True Crime"},
            //        new SelectListItem() { Text = "War"                   , Value="War" }
            //    };
            //    TagContentWarningListDDL = new List<SelectListItem> {
            //        new SelectListItem() { Text = "Blasphemy"         , Value="Blasphemy" },
            //        new SelectListItem() { Text = "Character Death "  , Value="Character Death" },
            //        new SelectListItem() { Text = "Explicit"          , Value="Explicit" },
            //        new SelectListItem() { Text = "Gore"              , Value="Gore" },
            //        new SelectListItem() { Text = "Out Dated Ideals"  , Value="Out Dated Ideals" },
            //        new SelectListItem() { Text = "Racism"            , Value="Racism" },
            //        new SelectListItem() { Text = "Rape/Non-consent"  , Value="Rape/Non-consent" },
            //        new SelectListItem() { Text = "Self Harm"         , Value="Self Harm"},
            //        new SelectListItem() { Text = "Sexism"            , Value="Sexism" },
            //        new SelectListItem() { Text = "Swearing"          , Value="Swearing" },
            //        new SelectListItem() { Text = "Violence"          , Value="Violence" }
                                                                      
            //    };
            //    TagTypesListDDL = new List<SelectListItem> {
            //       new SelectListItem() { Text = "Stand alone"   ,Value ="Stand alone"  },
            //       new SelectListItem() { Text = "Mini Series"   ,Value ="Mini Series"  },
            //       new SelectListItem() { Text = "Ongoing Story" ,Value ="Ongoing Story"},
            //       new SelectListItem() { Text = "Anthology"     ,Value ="Anthology"    },
            //       new SelectListItem() { Text = "Graphic Novel" ,Value ="Graphic Novel"},
            //       new SelectListItem() { Text = "Collection"    ,Value ="Collection"   },
            //       new SelectListItem() { Text = "Manga"         ,Value ="Manga"        }
            //    };
        }
    }
}
