$(document).ready( function(){

soundManager.setup({
    url: '../soundmanager/swf/',
    flashVersion: 9, // optional: shiny features (default = 8)
    // optional: ignore Flash where possible, use 100% HTML5 mode
    // preferFlash: false,
    onready: function () {
        // Ready to use; soundManager.createSound() etc. can now be called.
    }
});

    threeSixtyPlayer.config = {
        playNext: false, // stop after one sound, or play through list until end
        autoPlay: false, // start playing the first sound right away
        allowMultiple: false, // let many sounds play at once (false = one at a time)
        loadRingColor: '#ccc',// amount of sound which has loaded
        playRingColor: '#000', // amount of sound which has played
        backgroundRingColor: '#eee', // "default" color shown underneath everything else
        animDuration: 500,
        animTransition: Animator.tx.bouncy// http://www.berniecode.com/writing/animator.html
    }

});