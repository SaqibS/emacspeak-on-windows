# Emacspeak on Windows

## Overview

[Emacspeak](http://emacspeak.sourceforge.net) is an auditory interface to [Emacs](http://gnu.org/software/emacs). It intercepts all Emacs commands, and produces feedback using text-to-speech (TTS) and audio cues (earcons).

While Emacspeak is itself cross-platform, being written in Emacs Lisp, it uses speech servers to communicate with hardware/software speech synthesizers. To get it working on Windows, I have written a speech server for Windows, using the platform's native TTS/audio APIs. The server is actually just a command line program which reads commands from standard input, as defined in [this spec](http://emacspeak.sourceforge.net/info/html/TTS-Servers.html#TTS-Servers).

For convenience I have also included a preassembled archive in this repository, including a compiled version of Emacspeak and the Windows speech server, and a batch file to make it easier to start Emacs with Emacspeak loaded.

## Current Status

This first version of the TTS server was really a proof of concept so that I could use Emacspeak more. Depending how much I use it, and how much interest I get from others, I may spend the time to make it more robust. Of course, pull requests are welcome. In particular, using multiple voices is not currently implemented.

## Trying It Out

### Get Emacs

First, you must download Emacs for Windows. You can find details on the [Emacs homepage](http://gnu.org/software/emacs).

Personally, I have been working with Emacs 24.5, which is available at [http://ftp.gnu.org/gnu/emacs/windows/emacs-24.5-bin-i686-mingw32.zip]. In order to get EWW (the Emacs Web Wowser) to work, you also need the LibXML2 library, which you can get [here](http://sourceforge.net/projects/ezwinports/files).

### Use the Preassembled Archive

A quick way to get up and running may be to use the preassembled archive. However, this is just what works on my machine, and I can't provide support for this - it should continue working with new versions of Emacs 24.x, and with all versions of Windows, but I haven't tried that. If it doesn't work for you, you could try building from source, as described below.

Simply grab the preassembled/v42.zip file and uncompress it into your emacs directory. As the name implies, this is a compiled version of Emacspeak 42, plus the Windows speech server, and a batch file to help launch Emacspeak.

To start Emacs with Emacspeak enabled, run emacs_dir\bin\emacspeak.cmd. Though not necessary, I'd recommend starting this from an elevated command prompt (or tick the run-as-administrator checkbox when creating a shortcut).

### Build It Yourself

* First, download and unpack Emacs binaries and Emacspeak source code. To unpack the latter, you will need a tool like [7-Zip](http://7-zip.com).
* Then download and install [MSys](http://mingw.org/wiki/msys). Run "mingw-get msys-base" to get a basic command line environment, and then "mingw-get install msys-make" to download the GNU Make utility.
* Add the Emacs bin directory to your path using a command like "export PATH=/c/emacs-24.3/bin:$PATH".
* Change to the location where you unpacked Emacspeak, such as /c/Emacspeak-42.0.
* Then type the three commands:
 * make config
 * make emacspeak
 * make install prefix=/c/emacs
* Compile the speech server using the command line compiler that comes with Windows, or download a copy of Visual Studio Express. Copy it into the emacs/share/emacs/site-lisp/emacspeak/servers directory.
* Set the environment variable dtk_program=windows, and finally, start Emacs with a command like c:\emacs\bin\emacs -q -l c:\emacs\share\emacs\site-lisp\emacspeak\lisp\emacspeak-setup.el. Alternatively, load Emacspeak in your .emacs file (see below).

### Emacs Configuration

This is a purely optional step. While the emacspeak.cmd script is an easy way to get up and running, you may want to customize your Emacs configuration file, to load Emacspeak and set various settings.

For some reason, Emacs looks for your .emacs file in %home%, instead of %userprofile%. So, make sure you've defined that.

The start of my .emacs file is below (many more customizations can be made):

    (setenv "dtk_program" "windows")
    (load-file "c:/emacs/share/emacs/site-lisp/emacspeak/lisp/emacspeak-setup.el")
    (dtk-set-rate 5 t)
    (emacspeak-toggle-auditory-icons t)
    (emacspeak-sounds-select-theme "chimes-stereo/")

## Feedback

If you try this out, and particularly if you find it useful, please do get in touch. You can email "Me" at "SaqibShaikh.com".
