# Emacspeak on Windows

## Overview

[Emacspeak](http://emacspeak.sourceforge.net) is an auditory interface to [Emacs](http://gnu.org/software/emacs). It intercepts all Emacs commands, and produces feedback using text-to-speech (TTS) and audio cues (earcons).

While Emacspeak is itself cross-platform, being written in Emacs Lisp, it uses speech servers to communicate with hardware/software speech synthesizers. To get it working on Windows, I have written a speech server for Windows, using the platform's native TTS/audio APIs. The server is actually just a command line program which reads commands from standard input, as defined in [this spec](http://emacspeak.sourceforge.net/info/html/TTS-Servers.html#TTS-Servers).

For convenience I have also included a preassembled archive in this repository, including a compiled version of Emacspeak and the Windows speech server, and a batch file to make it easier to start Emacs with Emacspeak loaded.

## Current Status

This first version of the TTS server was really a proof of concept so that I could use Emacspeak more. Depending how much I use it, and how much interest I get from others, I may spend the time to make it more robust. Of course, pull requests are welcome.

* There is a bug where sometimes Emacspeak goes silent. This can be repro'd by pressing c-g (quit) twice, for example.
* At present there is no real speech queue, and commands are just dispatched in a single block. This means that, for example, if a queue of commands is dispatched to the TTS engine, and part way through a message is spoken immediately, we have lost the rest of the queue. However, in practicle usage this seems to be ok.
* Handling punctuation is delegated to the TTS engine, which isn't sufficient for our purposes. We should keep our own mapping of punctuation names, and which symbols should be spoken when the mode is set to none/some/all punctuation.
* Similarly, capitalization isn't currently handled.
* And finally, nor is playing tones or silence.

## Trying It Out

### Get Emacs

First, you must download Emacs for Windows. You can find details on the [Emacs homepage](http://gnu.org/software/emacs).

Personally, I have been working with Emacs 24.4, which is available at [http://ftp.gnu.org/gnu/emacs/windows/emacs-24.4-bin-i686-pc-mingw32.zip].

### Use the Preassembled Archive

A quick way to get up and running may be to use the preassembled archive. However, this is just what works on my machine, and I can't provide support for this - it should continue working with new versions of Emacs 24.x, and with all versions of Windows, but I haven't tried that. If it doesn't work for you, you could try building from source, as described below.

Simply grab the preassembled/v41.zip file and uncompress it into your emacs directory. As the name implies, this is a compiled version of Emacspeak 41, plus the Windows speech server, and a batch file to help launch Emacspeak.

To start Emacs with Emacspeak enabled, run emacs_dir\bin\emacspeak.cmd. Though not necessary, I'd recommend starting this from an elevated command prompt (or tick the run-as-administrator checkbox when creating a shortcut).

### Build It Yourself

* Note: I tried several times in the past to build Emacspeak on Windows, without success, due to missing APIs in the Windows port of Emacs. However, using Emacs 24.4 and Emacspeak 41, everything seems to work fine.
* First, download and unpack Emacs binaries and Emacspeak source code. To unpack the latter, you will need a tool like [7-Zip](http://7-zip.com).
* Then download and install [MSys](http://mingw.org/wiki/msys). Run "mingw-get msys-base" to get a basic command line environment, and then "mingw-get install msys-make" to download the GNU Make utility.
* Add the Emacs bin directory to your path using a command like "export PATH=/c/emacs-24.3/bin:$PATH".
* Change to the location where you unpacked Emacspeak, such as /c/Emacspeak-41.0
* Edit the makefile, and on line 123, replace "prefix = /usr" with a line like "prefix = /c/emacs".
 * Then type the three commands: make config; make emacspeak; make install.
 * Compile the speech server using the command line compiler that comes with Windows, or download a copy of Visual Studio Express. Copy it into the emacs/site-lisp/emacspeak/servers directory.
* Set the environment variable dtk_program=windows.
* Finally, start Emacs with a command like c:\emacs\bin\emacs -q -l c:\emacs\share\emacs\site-lisp\emacspeak\lisp\emacspeak-setup.el

## Feedback

If you try this out, and particularly if you find it useful, please do get in touch. You can email "Me" at "SaqibShaikh.com".
