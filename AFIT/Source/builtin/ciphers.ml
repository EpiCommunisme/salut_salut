(** Ciphers
    Built-in integer based ciphers.
*)

open Builtin
open Basic_arithmetics
open Power

(********** Cesar Cipher **********)

(** Cesar's cipher encryption
    @param k is an integer corresponding to key
    @param m word to cipher.
    @param b base ; for ASCII codes should be set to 255.
 *)
let rec encrypt_cesar k m b = match m with
    [] -> []
  | e::l -> modulo(e+k)(b+1)::encrypt_cesar k l b;;


(** Cesar's cipher decryption
    @param k is an integer corresponding to key
    @param m encrypted word.
    @param b base ; for ASCII code should be set to 255.
 *)
let rec decrypt_cesar k m b = match m with
    [] -> []
  |e::l -> modulo(e-k)(b+1)::decrypt_cesar k l b;;

(********** RSA Cipher **********)

(** Generate an RSA ciphering keys.
    Involved prime numbers need to be distinct. Output is a couple
    of public, private keys.
    @param p prime number
    @param q prime number
*)

let integer p q =
  let rec integer2 p q a b = match b with
    |b when b > a -> invalid_arg"c pas ca bro"
    |b when b > 1 && b < a && gcd a b = 1 -> b
    |b -> integer2 p q a (b+1);
  in integer2 p q ((p-1)*(q-1)) 2;;

let inv a b =
  let rec bezout2 a b (d,u,v,d',u',v') = match d' with
      0 -> if u < 0 then u + b else u
    |d' ->  let q = d/d' in bezout2  a b (d',u',v',d-q*d',u-q*u',v-q*v');
  in bezout2  a b (a,1,0,b,0,1);;

let generate_keys_rsa p q = if p = q then invalid_arg"p and q must be distinct" else
    let gene n z e d = ((n,e),(n,d))
    in gene (p*q) ((p-1)*(q-1)) (integer p q) (inv (integer p q) ((p-1)*(q-1)));;

(** Encryption using RSA cryptosystem.
    @param m integer hash of message
    @param pub_key a tuple (n, e) composing public key of RSA cryptosystem.
 *)
let encrypt_rsa m (n, e) = mod_power m e n;;

(** Decryption using RSA cryptosystem.
    @param m integer hash of encrypter message.
    @param pub_key a tuple (n, d) composing private key of RSA cryptosystem.
 *)
let decrypt_rsa m (n , d) = mod_power m d n;;

(********** ElGamal Cipher **********)

(** Generate ElGamal public data. Generates a couple (g, p)
    where p is prime and g having high enough order modulo p.
    @param p is prime having form 2*q + 1 for prime q.
 *)
let rec public_data_g p =
  let g = (Random.int (p-1)) in
  if (mod_power g 2 p) = 1 || g < 3
  then public_data_g p
  else (g,p);;
                                    

(** Generate ElGamal public data.
    @param pub_data a tuple (g, p) of public data for ElGamal cryptosystem.
 *)
let generate_keys_g (g, p) = let a = (p-1) in let b = Random.int a in ((mod_power g b p),b);;

(** ElGamal encryption process.
    @param msg message to be encrypted.
    @param pub_data a tuple (g, p) of ElGamal public data.
    @param kA ElGamal public key.
 *)
let encrypt_g msg (g, p) kA = let a = Random.int (p-1) in
                              let msg1 = mod_power g a p and msg2 = modulo ((mod_power kA a p) + msg) p
                              in (msg1,msg2);;

(** ElGamal decryption process.
    @param msg a tuple (msgA, msgB) forming an encrypted ElGamal message.
    @param a private key
    @param pub_data a tuple (g, p) of public data for ElGamal cryptosystem.
 *)
let decrypt_g (msgA, msgB) a (g, p) = (modulo ((prime_mod_power msgA (p-1-a) p) * msgB) p);;
