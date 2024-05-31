# PROG7311 POE PART 2 ST10150631 MICHAEL TURNER
# AGRI-ENERGY CONNECT PROTOTYPE web application 

![Screenshot 2024-05-31 120820](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/ef9c6ecd-a753-4222-9e78-8b5469a0414e)

## INTRODUCTION 
***Agri-Energy Connect*** is an app designed to connect farmers with an interest in green energy. It allows for buying and selling of green energy products, collaboration between users, the posting and sharing of educational resources and posting of photos and comments related to sustainable farming.

## Requirements:
* Microsoft Visual Studio 2022
* Internet Connection
* Web Browser (google chrome/ bing)
* .Net Framework 4.5 or Above
* Microsoft Windows 10 or newer
* Requires 1 GB of RAM (1.5 GB if running on a virtual machine)
* If x86 or AMD64/x64, requires a 1.6 GHz or faster processor
* Requires 1 GB of available hard disk space
* Visual Studio C# package
* NuGet Packages:
 ![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/34e04dcd-c5a6-43d7-9244-02441a9783ff)
* ASP.NET available from the visual studio installer

## How to run the app:
1. Unzip the folder called PROG7311_POE_PART2_MICHAEL_TURNER_ST10150631
2. Find the 'PROG7311 POE PART 2 ST10150631 MICHAEL TURNER.sin' file and open with visual studio wiuthin the unzipped folder
* ![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/c597fa22-f767-43ca-a9a5-16be277ddf06)
3. Run the .sin file using visual studio
  
## If Required:(
* In visual studio go to the tools tab and select NuGet package manager and search for the NuGet packages stated in the requirements
 ![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/77ab00cb-0c06-4ec0-9715-a4d367ec4fbc)
)
4. Build the app by clicking the build tab at in the top tool bar
  ![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/51d8800b-ef16-43e5-88ac-76a3fcc416e1)

5. Press the play button at the top of the screen if this does not work first time click the little arrow next to the play button and select your desired web browser and try again.
6. The app will load and you will now be able to use it !

The visual studio app is required as the web app is currently in development 

## User Roles 
Initially becuase there are only 2 user roles i considered storing a boolean in the database however by designating a Roles table this leaves space for scaling the app and adding two roles. 
The two roles currently are Employee and Farmer.
Employees can view all marketplace posts and purchase them while a farmer can manage posts.
Futhermore Employees can add farmers however farmers cannot add themselves or other farmers.
Employees can register.

## Application Features:

### Sign In & Registration 
* As stated in the application proposal for POE part 1 The value of the app will be shown upfront however to access these fetures a user must be logged in. Users who are not logged in have access to only registration and sign in features and can view posts but not create posts.
* ![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/307534d9-4612-48ce-a875-2953fdd69b67)

* ![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/dcdd8620-1209-4de0-941f-f0635a988d8a)

* To access features the user must register by clicking the register button on the navBar :
   ![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/304e8f66-d6a9-406b-bcad-8b8794528902)
  The registration password and detail requirements can be easily added but for the time being are not for the ease of use of the prototype currently the only limitations for a profile are username must be unique, no field can be blank and email must contain an '@' and a '.' .
  password length and complexity checks will be added before the app is released.
  *The user if they have an account can optionally log in by clicking the 'Login' button in the navbar. The log in is shared by both farmers and employees but this can be changed based on stakeholder requirements and feedback.
  ![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/54eeee17-fbcd-4432-85cf-3de86645e22e)
  * The user can sign out at any time by clicking the sign out button in the top right of the screen next to the search bar.
  * ![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/a5d94d9d-6d0e-41c9-a4b7-f913725f43b9)


  ### Homne page (Sustainable Farming Hub)
  * Once logged in the user can view posts and post to the home page by clicking the '+ New Post Button'
    Image of posts that can be text or include a picture:
    ![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/fcffe611-1a98-448d-9be3-0eb2b99b33ab)
    Image of new post function :
    ![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/cda704ec-06e8-430b-ba1a-3ede51718929)
    the post content cannot be blank and is required 
![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/8c34b13b-629d-4652-8445-536f342de253)
* Posts can be filtered by Topic by selecting the topic box under the page title selecting the topic and then pressing the apply filter button
* ![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/2ef6ec07-4774-4d07-a3b5-2eef47efef73)
* ![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/f9014a19-7904-435f-942f-c2094b21e568)
* The posts of that topic will then be displayed
* ![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/7b45b13b-9a3b-4279-a357-97322d6ec345)

  ### Green Energy marketplace
  * The green energy marketplace can be accessed by clicking 'Green Marketplace' on the navbar. Here Farmers can add and delete their own products while Employees can see all products, filter by category and production dates. The purchase function for employees will be available outside of the prototype stage.
    Farmer View: 
![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/d9346753-f55e-400e-8d7b-f51341e5da58)

![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/1fd31510-cd3d-4824-8565-a36ace40528b)

Employee View: 


### EducationHub 
* In the education Hub all users can add and  view courses. Courses must be uploaded with both an Image and A video and if the video plays is dependant on browser.
  ![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/34e4abd2-c3ff-4619-a8f8-64f0676f9aed)
![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/3293f82a-faa1-4240-a957-88ab52d291e6)

### Collaborations 
* Here users can discuss projects and best practices through a global chat however there is a feature that will be developed where users can add custom collaboration chats with selected users 
![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/705b6dfc-d594-4dd7-8d5f-c639485ce0b3)
![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/0380728c-3b56-41d8-a785-85166b2d2b76)
![image](https://github.com/ST10150631/Agri-Energy-Connect-App/assets/101188233/282ea1ed-6b35-4d9c-8953-45c3b0a464be)

## As this is a prototype the functionality is limited as it is made to provide more of an idea as to how the app will look and behave rather than being a fully developed.






    

  



