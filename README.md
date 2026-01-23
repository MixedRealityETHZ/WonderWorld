# V-ART

## Overview
You can find more information in our Final Report [here](https://github.com/wwwjones/V-ART/blob/main/V_ART_Final_Report.pdf) <br/>
You can find our teaser video [here](https://www.youtube.com/watch?v=2O7GKL1rN9Y)

The project started with the simple idea of visualizing 3D worlds created with Gaussian splatting in a Virtual Reality headset. From here, we developed the idea of exploring the worlds depicted in famous works of art, something we envisioned fitting seamlessly into a museum environment. With this basic concept, we performed an initial user study to collect feedback on what features users would like to see in such an app. We aimed to see if there was any interest in the concept, as well as gathering opinions on what users preferred: a fully virtual museum that users could explore from the comfort of their own home, or an augmented reality museum companion app that would improve a real-life museum-going experience. Participants overwhelmingly opted for an augmented reality companion application equipped with features such as museum navigation and painting information, as well as the possibility to view the 3D Gaussian splat worlds.

**V-ART**, therefore, is designed to act as an AR museum companion, built for the Meta Quest 3, allowing users to explore the museum freely and uninhibited, while having the options to view the museum map, collection of paintings, and immersive 3D worlds right at their fingertips. 

## Setup
1. Clone this repository
2. Open it in `Unity 6000.0.62f1`
3. Click `Fix All` in project validation window
4. Open `Main Scene` (assets folder)
5. Select: `File->Build Profiles->Android->Switch Platform`
6. Ensure the active scene is: `Main`
7. Select: `File->Build And Run` while metaquest is connected via USB to laptop
8. Generate QR codes (see QR codes section for the content)

## Using the app
The intended use of **V-ART** was created to function as follows:
1. The application opens to a brief tutorial screen instructing users how to interact with the application.
2. A hand gesture [1] opens the main menu, closing any other open menus. From this menu, users can open the list of available paintings, a museum map, or the tutorial screen again.
3. The painting list is a list that shows the paintings on display. It can be scrolled through, and tapping on a painting opens an information screen for that painting.
4. The painting information screen contains information that would typically be found in a museum, including title, artist, medium, and description. From here, the user can play an audio recording presenting them with more information on the painting. Additionally, the user can tap on a button to pinpoint the location of the painting on the map.
5. The map menu displays a map of the environment and has a 3D pin model to indicate the location of the selected painting. Interactive navigation is unfortunately outside the scope of this project.
6. When a user arrives at a painting, an adjacent QR code is automatically scanned, prompting the user to open the painting information menu, providing the user with the same information menu accessible through the painting list with the new addition of a button that virtually places the user within a corresponding Gaussian splat world based on the painting.
7. From within a Gaussian splat, the user can perform the same menu hand gesture [1] and close the view, opening the menu to continue exploration.

The option to view Gaussian splats only when physically in front of the painting is intentionally only accessible from the menu opened via the QR code, as we want to encourage the user to interact with the real artworks and systems of the application. Additionally, as the user will not be able to see their real-life surroundings while viewing gaussian splats, this is intended as a safety feature so museums can design their exhibits with this use in mind, keeping freely-wandering splat explorers to a minimum.

The final application is designed to be used without controllers, as we wanted to minimize the deviation from a normal museum-going experience and reduce necessary equipment. Interaction with the entire application is done via hand controls and gestures.

*[1] This is the default main menu hand gesture in the Meta Quest 3. Look at your left hand palm facing up. Do not stretch your fingers too much, try to keep them in a natural position. Wait for a virtual menu button to appear between your thumb and index finger. After it appears, pinch quickly.*

### QR Codes
**Please note this is a mandatory step in order to be able to visalize the splats**

In order to help us identify the painting the user is currently looking at we use QR codes. The QR codes contain text and encode the ID of the painting. The ID of the painting is just the index in the Painting Collection List (see `Editing the painting collection` section for more details).

To generate a QR code:
1. Use [QR.io](https://qr.io/) or any other tool you like
2. Make sure you select `Text`
3. Type the ID in the field (ex. `0`, `1`, ...). We know what you think, we do ID validations :smirk:

We have generated a QR code for the image with ID `0` for you: [QR code](https://github.com/wwwjones/V-ART/blob/main/qr_code_0.png)

## Editing the painting collection
**This is optional. The version of the app uploaded to the Github Repository already has a painting collection**

You can find the painting collection in the hierarchy under `UI->CollectionMenu->CanvasRoot->UIBackplate->GradientEffect->Header->Scroll View->Viewport->Content->Painting`

### The Painting Object
As **V-ART** is not only a Gaussian splat viewer, we collect more information on each viewable painting and store and process it internally in a C# **Painting** class. Each Painting instance contains a unique identification number, references to its actual image, coordinates of its real location within the museum, as well as its Gaussian splat representation. Additionally, it stores information that a user would expect to see in a museum environment: its title, artist, medium, and a description of the painting. This internal representation allows us to easily update and change a painting's data, for ease of use when "curating" a new museum. The Painting ID is used to identify it with QR codes placed in the museum, so that when a user approaches an artwork, the system can recognize which piece they are viewing and provide relevant information and actions. A Painting stores its coordinates so that the user can choose an artwork from the provided list and have its location displayed on a virtual map.

Further, we added the ability to assign an audio guide to a given painting. An audio source component is linked to the painting, which can then be played, paused, or restarted. The audio stops playing when the painting information panel is closed, but continues playing should a user decide to open a paintings respective panorama, allowing the experience to be further enhanced. 

It should be noted that this functionality isn't limited to plain audio guides. It could feature music, environmental noises befitting the scene or actual little narratives for entertainment purposes. 


### Generating Splats
First, if you want to add a new painting, you will need to generate a 3D world from that painting. We are using 3D Gaussian splats to represent the world.
1. To easily generate some new splats the tool that we used is [Marble](https://marble.worldlabs.ai/). Follow the work-flow on Marble and download the `500k` splats version in the `.ply` format. This version offers the best trade-off between quality and performance. **While our app could in theory work with any 3D Gaussian splats, we recommend using Marble to get consistent result with ours.**

2. To import the splats into Unity we are using this [plugin](https://github.com/aras-p/UnityGaussianSplatting). In the unity editor click on `tools->Gaussian Splats->Create GaussianSplatAsset' in the upper left taskbar. A pop-up will open, select a ply file, select a quality level, and then click create asset.


## Credits
* William Jones | [Linkedin](https://www.linkedin.com/in/william-jones-408115228/?originalSubdomain=ch)
* Andrei Cotor | [Linkedin](https://www.linkedin.com/in/andrei-cotor-2069951bb/)
* Berndt Uhlig | [Linkedin](https://www.linkedin.com/in/berndt-uhlig-6bb503336/)
* Albert Sandru | [Linkedin](https://www.linkedin.com/in/albert-sandru-185419246/)

Supervisors:
* Mihai Dusmanu
* Zuria Bauer
